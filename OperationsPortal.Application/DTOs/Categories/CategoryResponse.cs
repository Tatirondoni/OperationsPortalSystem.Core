namespace OperationsPortal.Application.Dtos.Categories;

public sealed class CategoryResponse
{
    public int CategoryId { get; init; }

    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}