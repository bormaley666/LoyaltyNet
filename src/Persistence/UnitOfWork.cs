using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Persistence;

/// <inheritdoc/>
/// <summary>
/// ctor
/// </summary>
/// <param name="db"></param>
public sealed class UnitOfWork(AppDbContext db, ILogger<UnitOfWork> logger) : IUnitOfWork
{
    private readonly AppDbContext _db = db ?? throw new ArgumentNullException(nameof(db));
    private readonly ILogger<UnitOfWork> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    /// <inheritdoc/>
    public Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken token = default)
        => BeginTransactionAsync(IsolationLevel.ReadCommitted, token);

    /// <inheritdoc/>
    public async Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken token = default)
    {
        if (_db.Database.CurrentTransaction is not null)
        {
            throw new InvalidOperationException("A transaction is already active. Nested transactions are not supported");
        }

        var transaction = await _db.Database.BeginTransactionAsync(isolationLevel, token);

        return new UnitOfWorkTransaction(transaction, _logger);
    }

    /// <inheritdoc/>
    public Task SaveChangesAsync(CancellationToken token = default)
        => _db.SaveChangesAsync(token);
}
