using System;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Categories;

/// <summary>
/// Код MCC-категории
/// </summary>
public readonly struct CategoryCode : IEquatable<CategoryCode>
{
    private readonly int _value;

    public static bool IsValid(int value) => value >= 0;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="value"></param>
    public CategoryCode(int value)
    {
        if (!IsValid(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        _value = value;
    }

    /// <summary>
    /// Строковое значение кода
    /// </summary>
    public int Value => _value;

    /// <inheritdoc/>
    public override string ToString() => Value.ToString();

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is MccCode other && Equals(other);

    /// <inheritdoc/>
    public override int GetHashCode() => _value;

    #region IEquatable
    public bool Equals(CategoryCode other) => _value == other._value;
    #endregion

    public static explicit operator int(CategoryCode code) => code.Value;

    public static explicit operator CategoryCode(int value) => new(value);

    public static bool operator ==(CategoryCode left, CategoryCode right) => left.Equals(right);

    public static bool operator !=(CategoryCode left, CategoryCode right) => !(left == right);
}
