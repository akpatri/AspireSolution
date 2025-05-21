using ProjectB.Data; // Add this using
using Microsoft.EntityFrameworkCore;
using MySqlConnector; // Add this using for MySqlConnection

namespace MigrationService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            // Register MySqlDBContext with the same name as in ProjectB
            builder.AddMySqlDbContext<MySqlDBContext>("ProjectBDb");

            builder.Services.AddHostedService<Worker>();

            var host = builder.Build();
            host.Run();
        }
    }
}