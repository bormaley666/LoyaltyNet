using System.Collections.Generic;
using System.Linq;
using LoyaltyNet.Application.References.Categories;
using LoyaltyNet.Application.References.Mccs;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Application.References.Categories;

internal static class CategoryDtoMapping
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto(category.Code, category.Name, category.Description, category.Mcc.ToDtoList());
    }

    public static IList<CategoryDto> ToDtoList(this IEnumerable<Category> categories)
    {
        return [.. categories.Select(m => m.ToDto())];
    }
}
