using System;
using System.Globalization;

namespace LoyaltyNet.Domain;

/// <summary>
/// Период
/// </summary>
public readonly struct YearMonth
{
    private readonly static string[] AcceptableDateTimeFormats = ["yyyyMM", "yyyy-MM"];
    private const int AsciiZeroCharOffset = 48;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsValid(int value) => ValidateValue(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsValid(string value) => ValidateValue(value, out var _);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public YearMonth(int value)
    {
        if (!ValidateValue(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = value;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public YearMonth(string value)
    {
        if (!ValidateValue(value, out var parsedValue))
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = parsedValue;
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value"></param>
    public YearMonth(DateTime value)
    {
        Value = value.Year * 100 + value.Month;
    }

    public DateTime StartDateUtc
    {
        get
        {
            var firstDayOfMonth = new DateTime(Year, Month, 1);

            return DateTime.SpecifyKind(firstDayOfMonth, DateTimeKind.Utc);
        }
    }

    public DateTime EndDateUtc
    {
        get
        {
            var lastDayOfMonth = new DateTime(Year, Month, 1)
                .AddMonths(1)
                .AddMicroseconds(-1);

            return DateTime.SpecifyKind(lastDayOfMonth, DateTimeKind.Utc);
        }
    }

    /// <summary>
    /// Целочисленное значение
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Год
    /// </summary>
    public int Year => Value / 100;

    /// <summary>
    /// Месяц
    /// </summary>
    public int Month => Value % 100;

    private static bool ValidateValue(int value)
    {
        Span<char> buffer = stackalloc char[6];

        var index = buffer.Length - 1;
        for (; index >= 0 && value > 0; index--)
        {
            buffer[index] = (char) (value % 10 + AsciiZeroCharOffset);
            value /= 10;
        }

        if (index == -1 && value == 0 &&
            DateTime.TryParseExact(buffer, AcceptableDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            return true;
        }

        return false;
    }

    private static bool ValidateValue(string value, out int parsedValue)
    {
        parsedValue = 0;

        var styles = DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite;

        if (value is not null && (value.Length == 6 || value.Length == 7) &&
            DateTime.TryParseExact(value, AcceptableDateTimeFormats, CultureInfo.InvariantCulture, styles, out var result))
        {
            parsedValue = result.Year * 100 + result.Month;

            return true;
        }

        return false;
    }
}
