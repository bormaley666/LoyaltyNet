using System;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Mccs.Services;

public interface IMccService
{
    public Task<Result<Mcc>> CreateMccAsync(MccCode code, string name, CancellationToken token = default);

    public Mcc UpdateMcc(Mcc mcc, string newName, DateTimeOffset? currentTime = default);
}
