using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Application;

public interface IUnitOfWork
{
    /// <summary>
    /// Начинает новую транзакцию с уровнем изоляции по умолчанию
    /// </summary>
    /// <param name="token">Токен отмены операции</param>
    /// <returns></returns>
    public Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken token = default);

    /// <summary>
    /// Начинает новую транзакцию с указанным уровнем изоляции
    /// </summary>
    /// <param name="isolationLevel">Уровень изоляции транзакции</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns></returns>
    public Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken token = default);

    /// <summary>
    /// Сохраняет все изменения, внесённые в контексте Unit of Work
    /// </summary>
    /// <param name="token">Токен отмены операции</param>
    /// <returns></returns>
    public Task SaveChangesAsync(CancellationToken token = default);
}
