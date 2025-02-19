using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos;

public record CreateItemDto
{
    [Required]
    public string Name { get; init; } = null!;

    [Required]
    public string Description { get; init; } = null!;

    [Range(0, 10000)]
    public decimal Price { get; init; }
}