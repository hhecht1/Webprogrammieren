using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace KomplexeEingaben
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ***********
            // builder
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthorization();

            builder.Services.AddControllersWithViews(options =>
            {
                options.ModelBinderProviders.Insert(0, new InvariantDecimalBinderProvider());
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            // *********
            // app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }




    public class InvariantDecimalBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            var type = context.Metadata.ModelType;
            if (type == typeof(decimal) || type == typeof(decimal?)
                || type == typeof(double) || type == typeof(double?))
            {
                return new InvariantNumberBinder();
            }
            return null;
        }
    }

    public class InvariantNumberBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext context)
        {
            var value = context.ValueProvider.GetValue(context.ModelName).FirstValue;
            if (string.IsNullOrEmpty(value))
                return Task.CompletedTask;

            var type = context.ModelMetadata.ModelType;

            if (type == typeof(decimal) || type == typeof(decimal?))
            {
                if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var d))
                    context.Result = ModelBindingResult.Success(d);
                else
                    context.ModelState.TryAddModelError(context.ModelName, "Ungültiger Wert.");
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                if (double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var d))
                    context.Result = ModelBindingResult.Success(d);
                else
                    context.ModelState.TryAddModelError(context.ModelName, "Ungültiger Wert.");
            }

            return Task.CompletedTask;
        }
    }
}
