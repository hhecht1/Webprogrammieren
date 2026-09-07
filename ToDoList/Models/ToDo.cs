using ToDoList.Models;

namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? ToDoText { get; set; }

        public bool IsDone { get; set; }
        public DateTime DateTime { get; set; }
    }
}