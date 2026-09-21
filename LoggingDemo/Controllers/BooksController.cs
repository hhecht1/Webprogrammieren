using LoggingDemo.Data;
using LoggingDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LoggingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController(AppDbContext appDbContext, ILogger<BooksController> loggerBook) : ControllerBase
    {
        private readonly ILogger<BooksController> _loggerBook = loggerBook;

        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model, IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                .CreateLogger("Add New Book");

            logger.LogInformation(12, "New Book added.");

            _loggerBook.LogInformation(7, "Created Book '{Name}' at {Time}", model.Title, DateTime.Now);
            //_loggerBook.LogInformation(7, $"Created Book '{model.Title}' at {DateTime.Now}");
            // Validierungen/Modifizierungen hier möglich

            //var author = new Author()
            //{
            //    Name = "Edgar Allen Po",
            //    Email = "edgar@allen.po"
            //};
            //model.Author = author;

            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            appDbContext.Books.AddRange(model);
            await appDbContext.SaveChangesAsync();

            return Ok(model);
        }
    }
}
