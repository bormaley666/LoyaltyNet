using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Application.References.Mccs;

public interface IMccCommandService
{
    /// <summary>
    /// Создание MCC
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="name">Наименование</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> CreateMccAsync(MccCode code, string name, CancellationToken token = default);

    /// <summary>
    /// Обновление MCC
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="newName">Наименование</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> UpdateMccAsync(MccCode code, string newName, CancellationToken token = default);

    /// <summary>
    /// Удаление MCC
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> DeactivateMccAsync(MccCode code, CancellationToken token = default);
}
