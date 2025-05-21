using ProjectB.Data;
using ProjectB.Repository;
using ProjectB.Services;

namespace ProjectB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Service Defaults registered
            builder.AddServiceDefaults();
            //Add RabbitMq
            builder.AddRabbitMQClient("messaging");

            //MySQL Client
            builder.AddMySqlDbContext<MySqlDBContext>("ProjectBDb"); // Name should be same as AppHost

            // Register CustomerRepository for DI
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<MessageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapDefaultEndpoints();

            app.MapControllers();

            app.Run();
        }
    }
}
