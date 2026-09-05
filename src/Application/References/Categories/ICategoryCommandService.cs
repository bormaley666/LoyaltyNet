using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Application.References.Categories;

public interface ICategoryCommandService
{
    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="name">Наименование категории</param>
    /// <param name="description">Описание категории</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> CreateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default);

    /// <summary>
    /// Обновление категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="name">Наименование категории</param>
    /// <param name="description">Описание категории</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> UpdateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default);

    /// <summary>
    /// Удаление категории
    /// </summary>
    /// <param name="code">Код категории</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> DeactivateCategoryAsync(CategoryCode code, CancellationToken token = default);

    /// <summary>
    /// Добавление MCC в категорию
    /// </summary>
    /// <param name="categoryCode">Код категории</param>
    /// <param name="code">Код MCC</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> AttachCategoryAsync(CategoryCode categoryCode, MccCode code, CancellationToken token = default);

    /// <summary>
    /// Удаление MCC из категории
    /// </summary>
    /// <param name="categoryCode">Код категории</param>
    /// <param name="code">Код MCC</param>
    /// <param name="token">Токен отмены</param>
    /// <returns></returns>
    public Task<Result> DetachCategoryAsync(CategoryCode categoryCode, MccCode code, CancellationToken token = default);
}
