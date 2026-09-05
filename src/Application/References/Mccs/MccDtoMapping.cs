using System.Collections.Generic;
using System.Linq;
using LoyaltyNet.Domain;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Application.References.Mccs;

internal static class MccDtoMapping
{
    public static MccDto ToDto(this Mcc mcc)
    {
        return new MccDto(mcc.Code, mcc.Name);
    }

    public static IList<MccDto> ToDtoList(this IEnumerable<Mcc> mccs)
    {
        return [.. mccs.Select(m => m.ToDto())];
    }

    public static IPagedList<MccDto> ToDtoPagedList(this IPagedList<Mcc> pagedMcc)
    {
        var dtos = pagedMcc.Items.ToDtoList();

        return new PagedList<MccDto>(dtos, pagedMcc.PageIndex, pagedMcc.PageSize, pagedMcc.TotalItems);
    }
}
