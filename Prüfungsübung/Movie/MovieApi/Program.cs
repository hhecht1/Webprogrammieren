using MovieApi.Models;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseHttpsRedirection();

var movies = new List<Movie>
{
    new Movie { Id = 1, Title = "Inception", Director = "Christopher Nolan", ReleaseYear = 2010 },
    new Movie { Id = 2, Title = "The Matrix", Director = "Lana Wachowski, Lilly Wachowski", ReleaseYear = 1999 },
    new Movie { Id = 3, Title = "Interstellar", Director = "Christopher Nolan", ReleaseYear = 2014 }
};


// Endpoints    

app.MapGet("/movies", () => movies);


app.MapGet("/movies/{id}", (int id) => movies.FirstOrDefault(m => m.Id == id));

app.MapPost("/movies", (Movie movie) =>
{
    movies.Add(movie);
    return Results.Created($"/movies/{movie.Id}", movie);
});

app.MapPut("/movies/{id}", (int id, Movie updatedMovie) =>
{
    var movie = movies.FirstOrDefault(m => m.Id == id);
    if (movie is null) return Results.NotFound();
    movie.Title = updatedMovie.Title;
    movie.Director = updatedMovie.Director;
    movie.ReleaseYear = updatedMovie.ReleaseYear;
    return Results.NoContent();
});


app.MapDelete("/movies/{id}", (int id) =>
{
    var movie = movies.FirstOrDefault(m => m.Id == id);
    if (movie is null) return Results.NotFound();
    movies.Remove(movie);
    return Results.NoContent();
});



app.Run();
