using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain;

namespace LoyaltyNet.Application.References.Mccs;

public interface IMccQueryService
{
    /// <summary>
    /// Получение списка MCC
    /// </summary>
    /// <param name="filter">Параметры фильтрации</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IList<MccDto>> GetMccAsync(string? query = null, CancellationToken token = default);

    /// <summary>
    /// Получение постраничного списка MCC
    /// </summary>
    /// <param name="pageIndex">Индекс страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <param name="filter">Параметры фильтрации</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IPagedList<MccDto>> GetPagedMccAsync(int pageIndex, int pageSize, string? query = null, CancellationToken token = default);
}
