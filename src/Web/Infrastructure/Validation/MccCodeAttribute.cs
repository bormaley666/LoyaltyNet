using System;
using System.ComponentModel.DataAnnotations;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Web.Infrastructure.Validation;

/// <summary>
/// Валидатор MCC кода
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class MccCodeAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var strValue = value.ToString();

        if (strValue is null || !MccCode.IsValid(strValue))
        {
            return new ValidationResult("Неверное значение MCC кода");
        }

        return ValidationResult.Success;
    }
}
