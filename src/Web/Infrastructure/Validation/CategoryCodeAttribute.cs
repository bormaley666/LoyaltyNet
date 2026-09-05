using System;
using System.ComponentModel.DataAnnotations;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Web.Infrastructure.Validation;

/// <summary>
/// Валидатор MCC кода
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class CategoryCodeAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (!int.TryParse(value.ToString(), out var intValue) || !CategoryCode.IsValid(intValue))
        {
            return new ValidationResult("Неверное значение кода категории");
        }

        return ValidationResult.Success;
    }
}
