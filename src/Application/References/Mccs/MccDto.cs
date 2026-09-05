using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Application.References.Mccs;

/// <summary>
/// MCC
/// </summary>
/// <param name="Code">Код</param>
/// <param name="Name">Наименование</param>
public sealed record MccDto(MccCode Code, string Name);
