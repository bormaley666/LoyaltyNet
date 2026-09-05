using System;
using System.ComponentModel.DataAnnotations;
using LoyaltyNet.Domain;

namespace LoyaltyNet.Web.Infrastructure.Validation;

/// <summary>
/// Валидатор <see cref="YearMonth"/>
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class YearMonthAttribute : ValidationAttribute
{
    /// <inheritdoc/>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        if (value is int intValue && YearMonth.IsValid(intValue))
        {
            return ValidationResult.Success;
        }
        else if (value is string stringValue && YearMonth.IsValid(stringValue))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Неверное значение параметра");
    }
}
