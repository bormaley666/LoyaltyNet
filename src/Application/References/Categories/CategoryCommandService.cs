using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Categories.Repositories;
using LoyaltyNet.Domain.References.Categories.Services;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Domain.References.Mccs.Repositories;

namespace LoyaltyNet.Application.References.Categories;

/// <inheritdoc/>
public sealed class CategoryCommandService(ICategoryService categoryService, ICategoryCommandRepository categoryRepository,
    IMccCommandRepository mccRepository, IUnitOfWork unitOfWork) : ICategoryCommandService
{
    private readonly ICategoryService _categoryService = categoryService;
    private readonly ICategoryCommandRepository _categoryRepository = categoryRepository;
    private readonly IMccCommandRepository _mccRepository = mccRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    /// <inheritdoc/>
    public async Task<Result> CreateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default)
    {
        var result = await _categoryService.CreateCategoryAsync(code, name, description, token);
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        await _categoryRepository.InsertCategoryAsync(result.Value, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> UpdateCategoryAsync(CategoryCode code, string name, string? description, CancellationToken token = default)
    {
        var category = await _categoryRepository.GetCategoryByCodeAsync(code, token);
        if (category is null)
        {
            return CategoryErrors.NotFound(code);
        }

        if (category.IsEquivalentTo(name, description))
        {
            return Result.Ok();
        }

        var newCategory = _categoryService.UpdateCategory(category, name, description);

        await _categoryRepository.InsertCategoryAsync(newCategory, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> DeactivateCategoryAsync(CategoryCode code, CancellationToken token = default)
    {
        var category = await _categoryRepository.GetCategoryByCodeAsync(code, token);
        if (category is null)
        {
            return CategoryErrors.NotFound(code);
        }

        var mccs = _categoryService.DeactivateCategory(category);
        foreach (var mcc in mccs)
        { 
            await _mccRepository.InsertMccAsync(mcc, token);        
        }

        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> AttachCategoryAsync(CategoryCode code, MccCode mccCode, CancellationToken token = default)
    {
        var category = await _categoryRepository.GetCategoryByCodeAsync(code, token);
        if (category is null)
        {
            return CategoryErrors.NotFound(code);
        }

        var mcc = await _mccRepository.GetMccByCodeAsync(mccCode, token);
        if (mcc is null)
        {
            return MccErrors.NotFound(mccCode);
        }

        var result = _categoryService.AttachToCategory(category, mcc);
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        await _mccRepository.InsertMccAsync(result.Value, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> DetachCategoryAsync(CategoryCode code, MccCode mccCode, CancellationToken token = default)
    {
        var category = await _categoryRepository.GetCategoryByCodeAsync(code, token);
        if (category is null)
        {
            return CategoryErrors.NotFound(code);
        }

        var mcc = await _mccRepository.GetMccByCodeAsync(mccCode, token);
        if (mcc is null)
        {
            return MccErrors.NotFound(mccCode);
        }

        var result = _categoryService.DetachFromCategory(category, mcc);
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }

        await _mccRepository.InsertMccAsync(result.Value, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }
}
