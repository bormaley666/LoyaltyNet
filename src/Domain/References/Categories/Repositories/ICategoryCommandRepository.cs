using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Domain.References.Categories.Repositories;

public interface ICategoryCommandRepository
{
    /// <summary>
    /// Получение категории по коду
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Category?> GetCategoryByCodeAsync(CategoryCode code, CancellationToken token = default);

    /// <summary>
    /// Добавление категории
    /// </summary>
    /// <param name="category">Категория</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task InsertCategoryAsync(Category category, CancellationToken token = default);
}
