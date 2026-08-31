namespace NoteApi.Services;

using NoteApi.Models;

public interface INoteService
{
    IReadOnlyList<Note> GetAll(string? search = null);
    Note? GetById(int id);
    Note Add(string title, string content);
    bool Delete(int id);
}