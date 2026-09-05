using System;
using System.Text.RegularExpressions;

namespace LoyaltyNet.Domain.Extensions;

public static class StringExtensions
{
    public static string TrimWhitespaces(this string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return Regex.Replace(value, @"\s+", " ", RegexOptions.None, new TimeSpan(0, 0, 5)).Trim();
    }

    public static string FormatLastFourMaskedCardNumber(this string? maskedNumber)
    {
        if (string.IsNullOrWhiteSpace(maskedNumber) || maskedNumber.Length <= 4)
        {
            return maskedNumber ?? "";
        }

        var lastFour = maskedNumber.Substring(maskedNumber.Length - 4, 4);

        return $"•••• {lastFour}";
    }
}
