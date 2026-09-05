using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Domain.References.Mccs.Repositories;
using LoyaltyNet.Domain.References.Mccs.Services;

namespace LoyaltyNet.Application.References.Mccs;

/// <inheritdoc/>
public sealed class MccCommandService(IMccService mccService, IMccCommandRepository repository, IUnitOfWork unitOfWork) : IMccCommandService
{
    private readonly IMccService _mccService = mccService;
    private readonly IMccCommandRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    /// <inheritdoc/>
    public async Task<Result> CreateMccAsync(MccCode code, string name, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var mcc = await _mccService.CreateMccAsync(code, name, token);
        if (mcc.IsFailed)
        {
            return Result.Fail(mcc.Errors);
        }

        await _repository.InsertMccAsync(mcc.Value, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> UpdateMccAsync(MccCode code, string newName, CancellationToken token = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);

        var mcc = await _repository.GetMccByCodeAsync(code, token);
        if (mcc is null)
        {
            return MccErrors.NotFound(code);
        }

        if (mcc.IsEquivalentTo(newName))
        {
            return Result.Ok();
        }

        var newMcc = _mccService.UpdateMcc(mcc, newName);

        await _repository.InsertMccAsync(newMcc, token);
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }

    /// <inheritdoc/>
    public async Task<Result> DeactivateMccAsync(MccCode code, CancellationToken token = default)
    {
        var mcc = await _repository.GetMccByCodeAsync(code, token);
        if (mcc is null)
        {
            return MccErrors.NotFound(code);
        }

        mcc.Deactivate();
        await _unitOfWork.SaveChangesAsync(token);

        return Result.Ok();
    }
}
