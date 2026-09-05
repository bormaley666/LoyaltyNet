using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Mccs.Repositories;

public interface IMccCommandRepository
{
    /// <summary>
    /// Получение MCC по коду
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Mcc?> GetMccByCodeAsync(MccCode code, CancellationToken token = default);

    /// <summary>
    /// Добавление MCC
    /// </summary>
    /// <param name="mcc">MCC</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task InsertMccAsync(Mcc mcc, CancellationToken token = default);
}
