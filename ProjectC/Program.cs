using ProjectC.Services;

namespace ProjectC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register RabbitMQ client for DI (name must match AppHost)
            builder.AddRabbitMQClient("messaging");

            // Register WorkerService as a hosted service
            builder.Services.AddHostedService<WorkerService>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.AddServiceDefaults();

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.MapDefaultEndpoints();

            app.Run();
        }
    }
}
