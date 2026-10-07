using OperationsPortal.Application.Dtos.Categories;
using OperationsPortal.Application.Exceptions;
using OperationsPortal.Application.Interfaces.Repositories;
using OperationsPortal.Domain.Entities;

namespace OperationsPortal.Application.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(
            cancellationToken);

        return categories
            .Select(ToResponse)
            .ToList();
    }

    public async Task<CategoryResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        return category is null ? null : ToResponse(category);
    }

    public async Task<CategoryResponse> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var name = request.Name.Trim();

        if (await _categoryRepository.ExistsByNameAsync(
                name,
                cancellationToken: cancellationToken))
        {
            throw new CategoryNameAlreadyExistsException(name);
        }
        var category = new Category
        {
            Name = name,
            Description = request.Description?.Trim(),
            IsActive = request.IsActive
        };

        await _categoryRepository.AddAsync(
            category,
            cancellationToken);

        return ToResponse(category);
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return false;
        }

        var name = request.Name.Trim();

        if (await _categoryRepository.ExistsByNameAsync(
                name,
                exceptCategoryId: category.CategoryId,
                cancellationToken: cancellationToken))
        {
            throw new CategoryNameAlreadyExistsException(name);
        }

        category.Name = name;   
        category.Description = request.Description?.Trim();
        category.IsActive = request.IsActive;

        await _categoryRepository.UpdateAsync(
            category,
            cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(
            id,
            cancellationToken);

        if (category is null)
        {
            return false;
        }

        await _categoryRepository.DeleteAsync(
            id,
            cancellationToken);

        return true;
    }

    private static CategoryResponse ToResponse(Category category)
    {
        return new CategoryResponse
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }
}