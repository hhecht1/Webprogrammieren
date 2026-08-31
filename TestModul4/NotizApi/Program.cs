using NoteApi.Services;
using NoteApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<INoteService, InMemoryNoteService>();


var app = builder.Build();
app.UseHttpsRedirection();




app.MapGet("/notes", (INoteService noteService, string? search) =>
{
    var notes = noteService.GetAll(search);
    return Results.Ok(notes);
}
);


app.MapGet("/notes/{id}", (int id, INoteService noteService) =>
{
    var noteId = noteService.GetById(id);


    if (noteId is not null)
    {
        return Results.Ok(noteId);
    }

    return Results.NotFound();

});

app.MapPost("/notes", (Note note, INoteService noteService) =>
{
    var createNote = noteService.Add(note.Title, note.Content);
    return Results.Created($"/notes/{createNote.Id}", createNote);

});

app.MapDelete("/notes/{id}", (int id, INoteService noteService) =>
{

    var deltetednote = noteService.Delete(id);
    if (!deltetednote)
    {
        return Results.NotFound();
    }
    return Results.NoContent();
});










app.Run();
