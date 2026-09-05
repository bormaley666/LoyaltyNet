using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LoyaltyNet.Application.References.Categories;

public interface ICategoryQueryService
{
    /// <summary>
    /// Получение списка категорий
    /// </summary>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IList<CategoryDto>> GetCategoriesAsync(CancellationToken token = default);
}
