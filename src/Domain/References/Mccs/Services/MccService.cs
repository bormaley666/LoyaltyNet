using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Mccs;
using LoyaltyNet.Domain.References.Mccs.Repositories;

namespace LoyaltyNet.Domain.References.Mccs.Services;

/// <inheritdoc/>
public sealed class MccService(IMccQueryRepository repository) : IMccService
{
    private readonly IMccQueryRepository _repository = repository;

    public async Task<Result<Mcc>> CreateMccAsync(MccCode code, string name, CancellationToken token = default)
    {
        if (await _repository.IsMccExistsAsync(code, token))
        {
            return MccErrors.AlreadyExists(code);
        }

        return new Mcc(code, name, null, DateTimeOffset.UtcNow);
    }

    public Mcc UpdateMcc(Mcc mcc, string newName, DateTimeOffset? currentTime = default)
    {
        var timestamp = currentTime ?? DateTimeOffset.UtcNow;

        var newMcc = new Mcc(mcc.Code, newName, mcc.Category, timestamp.AddMicroseconds(1));

        mcc.Deactivate(timestamp);

        return newMcc;
    }
}
