using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Mccs.Repositories;

public interface IMccQueryRepository
{
    /// <summary>
    /// Проверка на существование MCC
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<bool> IsMccExistsAsync(MccCode code, CancellationToken token = default);

    /// <summary>
    /// Получение списка MCC
    /// </summary>
    /// <param name="searchQuery"></param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IList<Mcc>> GetMccAsync(string? searchQuery = null, CancellationToken token = default);

    /// <summary>
    /// Получение постраничного списка MCC
    /// </summary>
    /// <param name="pageIndex">Индекс страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="searchQuery"></param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IPagedList<Mcc>> GetPagedMccAsync(int pageIndex, int pageSize, string? searchQuery = null, CancellationToken token = default);
}
