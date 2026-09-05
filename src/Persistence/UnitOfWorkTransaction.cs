using System;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Application;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace LoyaltyNet.Persistence;

/// <inheritdoc/>
internal sealed class UnitOfWorkTransaction : IUnitOfWorkTransaction
{
    private readonly IDbContextTransaction _transaction;
    private readonly ILogger<UnitOfWork> _logger;
    private bool _completed;
    private bool _disposed;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="db"></param>
    /// <param name="logger"></param>
    /// <exception cref="ArgumentNullException"></exception>
    internal UnitOfWorkTransaction(IDbContextTransaction transaction, ILogger<UnitOfWork> logger)
    {
        _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CommitAsync(CancellationToken token = default)
    {
        ThrowIfCompletedOrDisposed();

        try
        {
            await _transaction.CommitAsync(token);

            _completed = true;
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Transaction commit was cancelled");
            await InternalRollbackAsync(CancellationToken.None);

            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to commit transaction. Rolling back...");
            await InternalRollbackAsync(CancellationToken.None);

            throw;
        }
    }

    public async Task RollbackAsync(CancellationToken token = default)
    {
        ThrowIfCompletedOrDisposed();

        try
        {
            await InternalRollbackAsync(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to rollback transaction");

            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        if (!_completed)
        {
            _logger.LogWarning("Transaction was not explicitly completed. Rolling back automatically");
            await InternalRollbackAsync(CancellationToken.None);
        }

        try
        {
            await _transaction.DisposeAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to dispose transaction");
        }
        finally
        {
            _disposed = true;
        }
    }

    private async Task InternalRollbackAsync(CancellationToken token)
    {
        try
        {
            await _transaction.RollbackAsync(token);
            _completed = true;
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to rollback transaction");

            throw;
        }
    }

    private void ThrowIfCompletedOrDisposed()
    {
        if (_completed)
        {
            throw new InvalidOperationException("Transaction already completed");
        }

        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}
