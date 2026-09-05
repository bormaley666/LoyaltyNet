using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace LoyaltyNet.Web.Infrastructure.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DateFormatAttribute : ValidationAttribute
{
    private readonly string _format;
    private static readonly string[] AllowedFormats = ["yyyy-dd", "yyyy-dd-MM"];

    public DateFormatAttribute(string format)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(format);

        _format = format.Trim();

        if (!AllowedFormats.Any(e => string.Equals(e, _format, StringComparison.Ordinal)))
        {
            throw new ArgumentException($"Допустимые форматы: {string.Join(", ", AllowedFormats)}", nameof(format));
        }
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var strValue = value.ToString();

        if (string.IsNullOrEmpty(strValue) || !DateTime.TryParseExact(strValue, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            return new ValidationResult($"Неверный формат. Ожидается: '{_format}'");
        }

        return ValidationResult.Success;
    }
}