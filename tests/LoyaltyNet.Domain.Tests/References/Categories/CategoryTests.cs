using System;
using LoyaltyNet.Domain.References.Categories;
using Xunit;

namespace LoyaltyNet.Domain.Tests.References.Categories;

public class CategoryTests
{
    private static readonly DateTimeOffset FixedValidTo =
        new(2030, 1, 1, 12, 0, 0, TimeSpan.Zero);

    private static Category CreateCategory() =>
        new(
            code: new CategoryCode(1),
            name: "Test Category",
            description: "Test description",
            mcc: [],
            validFrom: new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero));

    [Fact]
    public void Deactivate_WhenActive_SetsValidToProvidedValue()
    {
        // Arrange
        var category = CreateCategory();
        Assert.Null(category.ValidTo);

        // Act
        category.Deactivate(FixedValidTo);

        // Assert
        Assert.NotNull(category.ValidTo);
        Assert.Equal(FixedValidTo, category.ValidTo);
    }
}
