using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Domain.References.Mccs.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence.References.Repositories;

/// <summary>
/// ctor
/// </summary>
/// <param name="db"></param>
public sealed class MccCommandRepository(AppDbContext db) : IMccCommandRepository
{
    private readonly AppDbContext _db = db;

    /// <inheritdoc/>
    public async Task<Mcc?> GetMccByCodeAsync(MccCode code, CancellationToken token = default)
    {
        var query = _db.Mcc
            .Include(m => m.Category)
            .Where(m => m.Code == code && m.ValidTo == null);

        return await query.FirstOrDefaultAsync(token);
    }

    /// <inheritdoc/>
    public async Task InsertMccAsync(Mcc mcc, CancellationToken token = default)
    {
        await _db.Mcc.AddAsync(mcc, token);
    }
}
