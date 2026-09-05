using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Categories.Services;

/// <summary>
/// Сервис категорий
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Создание категории
    /// </summary>
    /// <param name="code"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Result<Category>> CreateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default);

    /// <summary>
    /// Обновление категории
    /// </summary>
    /// <param name="category"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="currentTime"></param>
    /// <returns></returns>
    public Category UpdateCategory(Category category, string name, string? description, DateTimeOffset? currentTime = default);
    
    /// <summary>
    /// Деактивация категории
    /// </summary>
    /// <param name="category"></param>
    /// <param name="currentTime"></param>
    /// <returns></returns>
    public IList<Mcc> DeactivateCategory(Category category, DateTimeOffset? currentTime = default);

    /// <summary>
    /// Добавление MCC в категорию
    /// </summary>
    /// <param name="category"></param>
    /// <param name="mcc"></param>
    /// <param name="currentTime"></param>
    /// <returns></returns>
    public Result<Mcc> AttachToCategory(Category category, Mcc mcc, DateTimeOffset? currentTime = default);

    /// <summary>
    /// Удаление MCC из категории
    /// </summary>
    /// <param name="category"></param>
    /// <param name="mcc"></param>
    /// <param name="currentTime"></param>
    /// <returns></returns>
    public Result<Mcc> DetachFromCategory(Category category, Mcc mcc, DateTimeOffset? currentTime = default);
}
