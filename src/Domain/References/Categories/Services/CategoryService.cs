using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Categories.Repositories;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Categories.Services;

/// <inheritdoc/>
public sealed class CategoryService(ICategoryQueryRepository repository) : ICategoryService
{
    private readonly ICategoryQueryRepository _repository = repository;

    /// <inheritdoc/>
    public async Task<Result<Category>> CreateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default)
    {
        if (await _repository.IsCategoryExistsByCodeAsync(code, token))
        {
            return CategoryErrors.AlreadyExists(code);
        }

        var category = new Category(code, name, description, [], DateTimeOffset.UtcNow);

        return Result.Ok(category);
    }

    /// <inheritdoc/>
    public Category UpdateCategory(Category category, string name, string? description, DateTimeOffset? currentTime = default)
    {
        var timestamp = currentTime ?? DateTimeOffset.UtcNow;

        var mccs = new List<Mcc>();

        var newCategory = new Category(category.Code, name, description, mccs, timestamp);

        foreach (var mcc in category.Mcc)
        {
            var newMcc = new Mcc(mcc.Code, mcc.Name, newCategory, timestamp);

            mccs.Add(newMcc);
            mcc.Deactivate(timestamp.AddMicroseconds(-1));
        }

        category.Deactivate(timestamp.AddMicroseconds(-1));

        return newCategory;
    }

    /// <inheritdoc/>
    public Result<Mcc> AttachToCategory(Category category, Mcc mcc, DateTimeOffset? currentTime = default)
    {
        var timestamp = currentTime ?? DateTimeOffset.UtcNow;

        if (category.Contains(mcc))
        {
            return CategoryErrors.MccAlreadyInCategory(mcc.Code);
        }

        var newMcc = new Mcc(mcc.Code, mcc.Name, category, timestamp);

        mcc.Deactivate(timestamp.AddMicroseconds(-1));

        return newMcc;
    }

    /// <inheritdoc/>
    public Result<Mcc> DetachFromCategory(Category category, Mcc mcc, DateTimeOffset? currentTime = default)
    {
        var timestamp = currentTime ?? DateTimeOffset.UtcNow;

        if (!category.Contains(mcc))
        {
            return CategoryErrors.MccNotInCategory(mcc.Code);
        }

        var newMcc = new Mcc(mcc.Code, mcc.Name, null, timestamp);

        mcc.Deactivate(timestamp.AddMicroseconds(-1));

        return newMcc;
    }

    /// <inheritdoc/>
    public IList<Mcc> DeactivateCategory(Category category, DateTimeOffset? currentTime = default)
    {
        var timestamp = currentTime ?? DateTimeOffset.UtcNow;

        var mccs = new List<Mcc>();

        foreach (var mcc in category.Mcc)
        {
            var newMcc = new Mcc(mcc.Code, mcc.Name, null, timestamp);
            mccs.Add(newMcc);

            mcc.Deactivate(timestamp);
        }

        category.Deactivate(timestamp);

        return mccs;
    }
}
