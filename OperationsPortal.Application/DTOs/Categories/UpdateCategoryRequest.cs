using System.ComponentModel.DataAnnotations;

namespace OperationsPortal.Application.Dtos.Categories;

public sealed class UpdateCategoryRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; init; } = null!;

    [StringLength(255)]
    public string? Description { get; init; }

    public bool IsActive { get; init; }
}