using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence;

internal static class AppDbContextExtensions
{
    public static async Task ExecuteSqlAsync(this AppDbContext db, Func<DbConnection, Task> func, CancellationToken token = default)
    {
        if (db.Database.CurrentTransaction is not null)
        {
            await func(db.Database.GetDbConnection());

            return;
        }

        await using var tx = await db.Database.BeginTransactionAsync(token);

        try
        {
            await func(db.Database.GetDbConnection());

            await tx.CommitAsync(token);
        }
        catch
        {
            await tx.RollbackAsync(token);

            throw;
        }
    }

    public static async Task<T> ExecuteSqlAsync<T>(this AppDbContext db, Func<DbConnection, Task<T>> func, CancellationToken token = default)
    {
        if (db.Database.CurrentTransaction != null)
        {
            return await func(db.Database.GetDbConnection());
        }

        await using var tx = await db.Database.BeginTransactionAsync(token);

        try
        {
            var result = await func(db.Database.GetDbConnection());

            await tx.CommitAsync(token);

            return result;
        }
        catch
        {
            await tx.RollbackAsync(token);

            throw;
        }
    }
}
