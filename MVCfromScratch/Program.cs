using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Text;

namespace MVCfromScratch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseRouting();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.UseStaticFiles();


            app.MapGet("/htmlformular", async (HttpContext context) =>
            {
                await WriteHtml(context, $@"
                        <!doctype html>
                        <html>
                            <head><title>miniHTML</title></head>
                            <body>
                                <h1>Simple Framework</h1>
                                <br />
                                <form action=""/login"" method=""post"">
                                    <label for=""username"">User name:</label>
                                    <input type=""text"" id=""username"" name=""username"" required>
                                    <label for=""password"">Password:</label>
                                    <input type=""password"" id=""password"" name=""password"" required>
                                    <button type=""submit"">Login</button>
                                </form>
                            </body>
                        </html>");
            });

            app.MapPost("/login", async (HttpContext context) =>
            {
                //getting parameters
                var username = context.Request.Form["username"];
                var password = context.Request.Form["password"];

                // Authentication & Authorization / Validation
                if (username == "me" && password == "123")
                {
                    // work with html
                    var html = $@"
            <!doctype html>
            <html>
                <head><title>miniHTML</title></head>
                <body>
                    <h1>Simple Framework</h1>
                    <br />
                    Welcome to our simple framework!
                </body>
            </html>";
                    await WriteHtml(context, html);
                }
                else
                {
                    var html = $@"
            <!doctype html>
            <html>
                <head><title>miniHTML</title></head>
                <body>
                    <h1>Simple Framework</h1>
                    <br />
                    <form action=""/login"" method=""post"">
                        <label for=""username"">User name:</label>
                        <input type=""text"" id=""username"" name=""username"" required>
                        <label for=""password"">Password:</label>
                        <input type=""password"" id=""password"" name=""password"" required>
                        <button type=""submit"">Login</button>
                        <br />
                        <label style=""color:red"">Login failed!</label>
                    </form>
                </body>
            </html>";
                    await WriteHtml(context, html);
                }
            });




            app.Run();


            async Task WriteHtml(HttpContext context, string html)
            {
                context.Response.ContentType = MediaTypeNames.Text.Html;
                //context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(html);
                await context.Response.WriteAsync(html);
            }
        }
    }
}