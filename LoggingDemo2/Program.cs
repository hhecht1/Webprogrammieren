// Interface für Service-DI
public interface IMyService
{
    void LogCreation(string message);
}

// Klasse für Service-DI
public class MyService : IMyService
{
    public readonly int _serviceId;

    public MyService()
    {
        _serviceId = new Random().Next(100000, 999999);
    }

    public void LogCreation(string message)
    {
        Console.WriteLine($"[Msg from {message.ToUpper()}]   ServiceID: {_serviceId}");
    }
}

// Beispielhafte Nutzung des Services
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // builder.Services.AddScoped<IMyService, MyService>();
        // builder.Services.AddSingleton<IMyService, MyService>();
        builder.Services.AddTransient<IMyService, MyService>();


        var app = builder.Build();

        app.Use(async (context, next) =>
        {
            var myService = context.RequestServices.GetService<IMyService>();
            myService.LogCreation("1. Middleware");
            await next.Invoke();
        });

        app.Use(async (context, next) =>
        {
            var myService = context.RequestServices.GetService<IMyService>();
            myService.LogCreation("2. Middleware");
            await next.Invoke();
        });

        // app.MapGet("/", () => "Hello World!");

        app.MapGet("/service", (IMyService myService) =>
        {
            myService.LogCreation("Root");
            return Results.Ok("Check console");
        });


        app.Run();
    }
}