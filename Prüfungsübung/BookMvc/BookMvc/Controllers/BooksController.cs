using Microsoft.AspNetCore.Mvc;
using BookMvc.Models;

namespace BookMvc.Controllers;

public class BooksController : Controller
{
    private static readonly List<Book> books = new()
    {
        new Book
        {
            Id = 1,
            Title = "1984",
            Author = "George Orwell",
            Price = 9.99m
        },

        new Book
        {
            Id = 2,
            Title = "Herr der Ringe",
            Author = "J.R.R. Tolkien",
            Price = 19.99m
        },

        new Book
        {
            Id = 3,
            Title = "Faust",
            Author = "Johann Wolfgang von Goethe",
            Price = 15.60m
        }
    };

    // Alle Bücher anzeigen
    public IActionResult Index()
    {
        return View(books);
    }

    // Details eines Buches
    public IActionResult Details(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        return View(book);
    }

    // Create Formular anzeigen
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // Create Formular verarbeiten
    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        book.Id = books.Count == 0
            ? 1
            : books.Max(b => b.Id) + 1;

        books.Add(book);

        return RedirectToAction("Index");
    }

    // Edit Formular anzeigen
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        return View(book);
    }

    // Edit Formular verarbeiten
    [HttpPost]
    public IActionResult Edit(Book updatedBook)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedBook);
        }

        var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);

        if (book is null)
        {
            return NotFound();
        }

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Price = updatedBook.Price;

        return RedirectToAction("Index");
    }

    // Buch löschen
    public IActionResult Delete(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);

        if (book is null)
        {
            return NotFound();
        }

        books.Remove(book);

        return RedirectToAction("Index");
    }
}