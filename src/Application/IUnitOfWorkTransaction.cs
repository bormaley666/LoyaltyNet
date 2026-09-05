using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Application;

/// <summary>
/// Представляет транзакцию в рамках паттерна Unit of Work
/// </summary>
public interface IUnitOfWorkTransaction : IAsyncDisposable
{
    /// <summary>
    /// Фиксирует все изменения, выполненные в рамках текущей транзакции
    /// </summary>
    /// <param name="token">Токен отмены операции</param>
    /// <returns></returns>
    public Task CommitAsync(CancellationToken token = default);

    /// <summary>
    /// Откатывает все изменения, выполненные в рамках текущей транзакции
    /// </summary>
    /// <param name="token">Токен отмены операции</param>
    /// <returns></returns>
    public Task RollbackAsync(CancellationToken token = default);
}
