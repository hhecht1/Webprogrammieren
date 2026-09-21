//using System.ComponentModel.DataAnnotations;

namespace LoggingDemo.Models
{
    public class Book
    {
        //[Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        //public int Country { get; set; }

        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
