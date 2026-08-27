using BookApi.Models;
using BookApi.Dtos;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book { Id = 1, Title = "1984", Author = "George Orwell", Price = 9.99m },
    new Book { Id = 2, Title = "Brave New World", Author = "Aldous Huxley", Price = 14.99m },
    new Book { Id = 3, Title = "Fahrenheit 451", Author = "Ray Bradbury", Price = 19.99m },
    new Book { Id = 4, Title = "The Catcher in the Rye", Author = "J.D. Salinger", Price = 12.99m },
    new Book { Id = 5, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 15.99m },
    new Book { Id = 6, Title = "Moby-Dick", Author = "Herman Melville", Price = 18.99m },
    new Book { Id = 7, Title = "Pride and Prejudice", Author = "Jane Austen", Price = 11.99m },
    new Book { Id = 8, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 13.99m },
    new Book { Id = 9, Title = "The Hobbit", Author = "J.R.R. Tolkien", Price = 17.99m },
    new Book { Id = 10, Title = "War and Peace", Author = "Leo Tolstoy", Price = 21.99m }

};


// Root
app.MapGet("/", () => "Book API");


// Endpoints

var group = app.MapGroup("/books");


// GET alle Bücher
group.MapGet("/", () =>
{
    return Results.Ok(books);
});


// GET Buch nach ID
group.MapGet("/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);

    if (book is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(book);
});


// POST neues Buch mit DTO ansprechbar 
group.MapPost("/", (CreateBookDto newBookDto) =>
{
    var newBook = new Book
    {
        Id = books.Count == 0 ? 1 : books.Max(b => b.Id) + 1,
        Title = newBookDto.Title,
        Author = newBookDto.Author,
        Price = newBookDto.Price
    };

    books.Add(newBook);

    return Results.Created(
        $"/books/{newBook.Id}",
        newBook
    );
});

// //Post mit der id 1
// group.MapPost("/1", (Book newBook) =>
// {
//     newBook.Id = 1;

//     books.Add(newBook);

//     return Results.Created(
//         $"/books/{newBook.Id}",
//         newBook
//     );
// });


// PUT Buch ändern
group.MapPut("/{id}", (int id, UpdateBookDto updatedBook) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);

    if (book is null)
    {
        return Results.NotFound();
    }

    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    book.Price = updatedBook.Price;

    return Results.NoContent();
});

// Ohne mapGroup
// app.MapPut("/books/{id}", (int id, UpdateBookDto updatedBook) =>
// {
//     var book = books.FirstOrDefault(b => b.Id == id);

//     if (book is null)
//     {
//         return Results.NotFound();
//     }

//     book.Title = updatedBook.Title;
//     book.Author = updatedBook.Author;
//     book.Price = updatedBook.Price;

//     return Results.NoContent();
// });


// DELETE Buch mit mapGroup
group.MapDelete("/{id}", (int id) =>
{
    var book = books.FirstOrDefault(b => b.Id == id);

    if (book is null)
    {
        return Results.NotFound();
    }

    books.Remove(book);

    return Results.NoContent();
});
// Ohne mapGroup
// app.MapDelete("/books/{id}", (int id) =>
// {
//     var book = books.FirstOrDefault(b => b.Id == id);

//     if (book is null)
//     {
//         return Results.NotFound();
//     }

//     books.Remove(book);

//     return Results.NoContent();
// });


app.Run();