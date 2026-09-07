// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using SimpleMvc.Models;

namespace SimpleMvc.Controllers
{
    public class HomeController : Controller
    {
        // Simuliert eine Datenbankabfrage
        private static readonly List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Der Alchimist", Author = "Paulo Coelho", Price = 12.99m },
            new Book { Id = 2, Title = "1984", Author = "George Orwell", Price = 9.99m },
            new Book { Id = 3, Title = "Brave New World", Author = "Aldous Huxley", Price = 11.99m },
            new Book { Id = 4, Title = "Fahrenheit 451", Author = "Ray Bradbury", Price = 10.99m },
            new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Price = 8.99m }
        };

        // URL: /Home/Index oder einfach /
        public IActionResult Index()
        {
            // Daten vom "Modell" holen und an die View übergeben
            return View(_books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // URL: /Home/Details/1
        public IActionResult Details(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        public IActionResult Remove(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            _books.Remove(book);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = _books.Max(b => b.Id) + 1;
                _books.Add(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}