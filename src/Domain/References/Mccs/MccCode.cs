using System;
using System.Text.RegularExpressions;

namespace LoyaltyNet.Domain.References.Mccs;

/// <summary>
/// MCC-код
/// </summary>
public readonly struct MccCode : IEquatable<MccCode>
{
    private static readonly Regex _template =
        new(@"^\s*[0-9]{4}\s*$", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

    private readonly string _value;

    public static bool IsValid(string value) =>
        value is not null && _template.IsMatch(value);

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value"></param>
    public MccCode(string value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        _value = value.Trim();
    }

    /// <summary>
    /// Строковое значение MCC-кода
    /// </summary>
    public string Value => _value;

    /// <inheritdoc/>
    public override string ToString() => Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is MccCode other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => _value?.GetHashCode() ?? 0;

    #region IEquatable
    public bool Equals(MccCode other) => _value == other._value;
    #endregion

    public static explicit operator string(MccCode code) => code.Value;

    public static explicit operator MccCode(string value) => new(value);

    public static bool operator ==(MccCode left, MccCode right) => left.Equals(right);
    
    public static bool operator !=(MccCode left, MccCode right) => !(left == right);
}
