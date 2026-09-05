using FluentResults;

namespace LoyaltyNet.Domain.References.Mccs;

public class MccErrors
{
    public static Error AlreadyExists(MccCode code) =>
        new Error($"MCC с кодом '{code}' уже существует")
            .WithMetadata("code", "mcc_already_exists");

    public static Error NotFound(MccCode code) =>
        new Error($"MCC с кодом '{code}' не найден")
            .WithMetadata("code", "mcc_not_found");
}
