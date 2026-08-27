using System.ComponentModel.DataAnnotations;


namespace BookApi.Dtos
{
    public record UpdateBookDto(
        [Required] string? Title,
        [Required] string? Author,
        [Required] decimal Price
    );
}