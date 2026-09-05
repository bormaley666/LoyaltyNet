using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain.References.Categories.Repositories;

namespace LoyaltyNet.Application.References.Categories;

/// <inheritdoc/>
public sealed class CategoryQueryService(ICategoryQueryRepository repository) : ICategoryQueryService
{
    private readonly ICategoryQueryRepository _repository = repository;

    /// <inheritdoc/>
    public async Task<IList<CategoryDto>> GetCategoriesAsync(CancellationToken token = default)
    {        
        var categories = await _repository.GetCategoriesAsync(token);

        return categories.ToDtoList();
    }
}
