using System.Collections.Generic;
using System.Linq;
using LoyaltyNet.Application.References.Mccs;

namespace LoyaltyNet.Web.References.Mccs.Models;

public static class MccModelMapping
{
    public static MccModel ToModel(this MccDto mcc)
    {
        return new MccModel()
        {
            Code = mcc.Code.Value,
            Name = mcc.Name
        };
    }

    public static IList<MccModel> ToModelList(this IList<MccDto> mccs)
    {
        return [.. mccs.Select(e => e.ToModel())];
    }
}
