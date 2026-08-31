namespace NoteApi.Models;

public record Note
    (
    int Id,
    string Title,
    string Content
    );