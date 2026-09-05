using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Categories.Repositories;

namespace LoyaltyNet.Persistence.References.Repositories;

/// <inheritdoc/>
public sealed class CategoryQueryRepository(AppDbContext db) : ICategoryQueryRepository
{
    private readonly AppDbContext _db = db;

    /// <inheritdoc/>
    public async Task<Category?> GetCategoryByCodeAsync(CategoryCode code, DateTimeOffset? validAt, CancellationToken token = default)
    {
        validAt ??= DateTimeOffset.UtcNow;

        return await _db.Categories
            .AsNoTracking()
            .Include(c => c.Mcc.Where(m => m.ValidFrom <= validAt &&
                (m.ValidTo == null || m.ValidTo > validAt)))
            .Where(c => c.Code == code &&
                c.ValidFrom <= validAt &&
                (c.ValidTo == null || c.ValidTo > validAt))
            .FirstOrDefaultAsync(token);
    }

    /// <inheritdoc/>
    public async Task<IList<Category>> GetCategoriesAsync(CancellationToken token = default)
    {
        var query = _db.Categories
            .AsNoTracking()
            .Include(c => c.Mcc.Where(m => m.ValidTo == null))
            .Where(c => c.ValidTo == null);

        return await query.ToListAsync(token);
    }

    /// <inheritdoc/>
    public async Task<bool> IsCategoryExistsByCodeAsync(CategoryCode code, CancellationToken token = default)
    {
        return await _db.Categories
            .AnyAsync(c => c.Code == code && c.ValidTo == null, token);
    }
}
