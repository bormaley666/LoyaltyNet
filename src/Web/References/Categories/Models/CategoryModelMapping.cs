using System.Collections.Generic;
using System.Linq;
using LoyaltyNet.Application.References.Categories;
using LoyaltyNet.Web.References.Categories.Models;
using LoyaltyNet.Web.References.Mccs.Models;

namespace LoyaltyNet.Web.References.Categories.Models;

public static class CategoryModelMapping
{
    public static CategoryModel ToModel(this CategoryDto category)
    {
        return new CategoryModel()
        {
            Code = category.Code.Value,
            Name = category.Name,
            Description = category.Description,
            Mcc = category.Mcc.ToModelList()
        };
    }

    public static IList<CategoryModel> ToModelList(this IList<CategoryDto> categories)
    {
        return [.. categories.Select(e => e.ToModel())];
    }
}
