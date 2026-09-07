using System.ComponentModel.DataAnnotations;

namespace MVCfromScratch.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}