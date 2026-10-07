using Microsoft.EntityFrameworkCore;
using OperationsPortal.Application.Interfaces.Repositories;
using OperationsPortal.Domain.Entities;
using OperationsPortal.Infrastructure.Data;

using CategoryEntity = OperationsPortal.Infrastructure.Data.Entities.Category;

namespace OperationsPortal.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly OperationsPortalDbContext _context;

    public CategoryRepository(OperationsPortalDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Category>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Select(category => new Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(category => category.CategoryId == id)
            .Select(category => new Category
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                IsActive = category.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistsByNameAsync(
    string name,
    int? exceptCategoryId = null,
    CancellationToken cancellationToken = default)
    {
        var query = _context.Categories
            .AsNoTracking()
            .Where(category => category.Name == name);

        if (exceptCategoryId.HasValue)
        {
            query = query.Where(category =>
                category.CategoryId != exceptCategoryId.Value);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        var entity = new CategoryEntity
        {
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };

        await _context.Categories.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        category.CategoryId = entity.CategoryId;
    }

    public async Task UpdateAsync(
        Category category,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Categories
            .FirstOrDefaultAsync(
                item => item.CategoryId == category.CategoryId,
                cancellationToken);

        if (entity is null)
        {
            return;
        }

        entity.Name = category.Name;
        entity.Description = category.Description;
        entity.IsActive = category.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Categories
            .FirstOrDefaultAsync(
                category => category.CategoryId == id,
                cancellationToken);

        if (entity is null)
        {
            return;
        }

        _context.Categories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}