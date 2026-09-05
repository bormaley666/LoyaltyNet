using System.Collections.Generic;
using LoyaltyNet.Application.References.Mccs;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Application.References.Categories;

public sealed record CategoryDto(CategoryCode Code, string Name, string? Description, IList<MccDto> Mcc);
