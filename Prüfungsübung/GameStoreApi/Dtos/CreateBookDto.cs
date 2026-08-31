using System.ComponentModel.DataAnnotations;

namespace BookApi.Dtos
{
    public record CreateBookDto(
        [Required] string? Title,
        [Required] string? Author,
        [Required] decimal Price
    );
}