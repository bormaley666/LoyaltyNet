using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence;

/// <inheritdoc/>
public sealed class DatabaseMigrator(AppDbContext db) : IDatabaseMigrator
{
    /// <inheritdoc/>
    public async Task ApplyMigrationsAsync()
    {
        if (db.Database.IsRelational())
        {
            await db.Database.MigrateAsync();
        }
    }
}