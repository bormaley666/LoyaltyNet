using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Categories.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence.References.Repositories;

/// <summary>
/// ctor
/// </summary>
/// <param name="db"></param>
public sealed class CategoryCommandRepository(AppDbContext db) : ICategoryCommandRepository
{
    private readonly AppDbContext _db = db;

    /// <inheritdoc/>
    public async Task<Category?> GetCategoryByCodeAsync(CategoryCode code, CancellationToken token = default)
    {
        return await _db.Categories
            .Include(c => c.Mcc.Where(m => m.ValidTo == null))
            .Where(c => c.Code == code && c.ValidTo == null)
            .FirstOrDefaultAsync(token);
    }

    /// <inheritdoc/>
    public async Task InsertCategoryAsync(Category category, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(category);

        await _db.Categories.AddAsync(category, token);
    }
}
