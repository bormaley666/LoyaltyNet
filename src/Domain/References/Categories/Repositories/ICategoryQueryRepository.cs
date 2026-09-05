using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Domain.References.Categories.Repositories;

public interface ICategoryQueryRepository
{
    /// <summary>
    /// Получение категории по коду
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="validAt">Момент времени</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Category?> GetCategoryByCodeAsync(CategoryCode code, DateTimeOffset? validAt, CancellationToken token = default);

    /// <summary>
    /// Получение списка категорий
    /// </summary>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<IList<Category>> GetCategoriesAsync(CancellationToken token = default);

    /// <summary>
    /// Проверка существования категории с указанным кодом
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>true если категория существует</returns>
    public Task<bool> IsCategoryExistsByCodeAsync(CategoryCode code, CancellationToken token = default);
}
