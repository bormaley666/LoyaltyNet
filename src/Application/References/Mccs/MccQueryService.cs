using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LoyaltyNet.Domain;
using LoyaltyNet.Domain.References.Mccs.Repositories;

namespace LoyaltyNet.Application.References.Mccs;

/// <inheritdoc/>
public sealed class MccQueryService(IMccQueryRepository repository) : IMccQueryService
{
    private readonly IMccQueryRepository _repository = repository;

    /// <inheritdoc/>
    public async Task<IList<MccDto>> GetMccAsync(string? searchQuery = null, CancellationToken token = default)
    {
        var mccs = await _repository.GetMccAsync(searchQuery, token);

        return mccs.ToDtoList();
    }

    /// <inheritdoc/>
    public async Task<IPagedList<MccDto>> GetPagedMccAsync(int pageIndex, int pageSize, string? searchQuery = null, CancellationToken token = default)
    {
        var pagedMcc = await _repository.GetPagedMccAsync(pageIndex, pageSize, searchQuery, token);

        return pagedMcc.ToDtoPagedList();
    }
}
