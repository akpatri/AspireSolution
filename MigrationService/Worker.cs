using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectB.Data; // Add this using
using Microsoft.EntityFrameworkCore;

namespace MigrationService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Wait for MySQL to be available before proceeding
            var maxAttempts = 10;
            var delay = TimeSpan.FromSeconds(5);
            var attempt = 0;
            bool dbReady = false;

            while (attempt < maxAttempts && !dbReady)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<MySqlDBContext>();
                        var databaseName = dbContext.Database.GetDbConnection().Database;
                        var connection = dbContext.Database.GetDbConnection();

                        // Open a connection to MySQL server (without specifying database)
                        var masterConnStr = new MySqlConnector.MySqlConnectionStringBuilder(connection.ConnectionString)
                        {
                            Database = ""
                        }.ToString();
                        using var masterConn = new MySqlConnector.MySqlConnection(masterConnStr);
                        await masterConn.OpenAsync(stoppingToken);
                        using var cmd = masterConn.CreateCommand();
                        cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`;";
                        await cmd.ExecuteNonQueryAsync(stoppingToken);

                        _logger.LogInformation("Applying migrations...");
                        await dbContext.Database.MigrateAsync(stoppingToken);
                        _logger.LogInformation("Migrations applied.");
                        dbReady = true;
                    }
                }
                catch (Exception ex)
                {
                    attempt++;
                    _logger.LogWarning(ex, "Database not ready, retrying in {Delay}s... (Attempt {Attempt}/{MaxAttempts})", delay.TotalSeconds, attempt, maxAttempts);
                    await Task.Delay(delay, stoppingToken);
                }
            }

            if (!dbReady)
            {
                _logger.LogError("Failed to connect to MySQL and apply migrations after {MaxAttempts} attempts.", maxAttempts);
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
