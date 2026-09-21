using LoggingDemo.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace LoggingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddHttpLogging(options =>
            {
                // Configure HTTP logging options here if needed
            });

            // builder.Logging.AddJsonConsole(options =>
            // {
            //     options.JsonWriterOptions = new() { Indented = true };
            // });

            //****************
            // app
            var app = builder.Build();

            app.Logger.LogInformation(5, "We are ready to go.");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                Console.WriteLine("[CLI] IsDevelopment");
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}
