using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Domain.References.Mccs.Repositories;
using LoyaltyNet.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence.References.Repositories;

/// <summary>
/// ctor
/// </summary>
/// <param name="db"></param>
public sealed class MccQueryRepository(AppDbContext db) : IMccQueryRepository
{
    private readonly AppDbContext _db = db;

    /// <inheritdoc/>
    public async Task<bool> IsMccExistsAsync(MccCode code, CancellationToken token = default)
    {
        return await _db.Mcc
            .Where(m => m.Code == code && m.ValidTo == null)
            .AnyAsync(token);
    }

    /// <inheritdoc/>
    public async Task<IList<Mcc>> GetMccAsync(string? searchQuery = null, CancellationToken token = default)
    {
        var query = _db.Mcc
            .AsNoTracking()
            .Where(m => m.ValidTo == null)
            .OrderBy(m => m.Code);

        return await ApplyFilter(query, searchQuery)
            .ToListAsync(token);
    }

    /// <inheritdoc/>
    public async Task<IPagedList<Mcc>> GetPagedMccAsync(int pageIndex, int pageSize, string? searchQuery = null, CancellationToken token = default)
    {
        var query = _db.Mcc
            .AsNoTracking()
            .Where(m => m.ValidTo == null)
            .OrderBy(m => m.Code);

        return await ApplyFilter(query, searchQuery)
            .ToPagedListAsync(pageIndex, pageSize, token);
    }

    private static IQueryable<Mcc> ApplyFilter(IQueryable<Mcc> query, string? searchQuery)
    {
        if (searchQuery is not null)
        {
            query = query.Where(e => ((string)e.Code).StartsWith(searchQuery) || EF.Functions.ILike(e.Name, $"%{searchQuery}%"));
        }

        return query;
    }
}
