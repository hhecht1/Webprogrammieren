namespace NoteApi.Services;

using NoteApi.Models;

public class InMemoryNoteService : INoteService
{
    private readonly List<Note> _notes = new()
    {
        new(1, "Einkaufen", "Milk, Bread, Eggs"),
        new(2, "Meeting", "Weekly sync at 10am"),
        new(3, "Idee", "Build a todo app")
    };
    private int _nextId = 4;

    public IReadOnlyList<Note> GetAll(string? search = null)
        => string.IsNullOrWhiteSpace(search)
            ? _notes.ToList()
            : _notes.Where(n => n.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

    public Note? GetById(int id) => _notes.FirstOrDefault(n => n.Id == id);
    public Note Add(string title, string content)
    {
        var note = new Note(_nextId++, title, content);
        _notes.Add(note);
        return note;
    }
    public bool Delete(int id) => _notes.RemoveAll(n => n.Id == id) > 0;
}