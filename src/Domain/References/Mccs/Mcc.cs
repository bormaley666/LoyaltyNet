using System;
using LoyaltyNet.Domain.References.Categories;

namespace LoyaltyNet.Domain.References.Mccs;

/// <summary>
/// MCC
/// </summary>
public sealed class Mcc : BaseEntity
{
    /// <summary>
    /// EF Core ctor
    /// </summary>
    private Mcc() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="code">Код</param>
    /// <param name="name">Наименование</param>
    /// <param name="category">Категория</param>
    /// <param name="validFrom"></param>
    public Mcc(MccCode code, string name, Category? category, DateTimeOffset validFrom)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Code = code;
        Name = name.Trim();
        CategoryId = category?.Id;
        Category = category;
        ValidFrom = validFrom;
    }

    /// <summary>
    /// Код
    /// </summary>
    public MccCode Code { get; private set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// Идентификатор категории
    /// </summary>
    public Guid? CategoryId { get; private set; }

    /// <summary>
    /// Категория
    /// </summary>
    public Category? Category { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTimeOffset ValidFrom { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTimeOffset? ValidTo { get; private set; }

    public bool IsEquivalentTo(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return Name == name.Trim();
    }

    /// <summary>
    /// Деактивация
    /// </summary>
    public void Deactivate(DateTimeOffset? validTo = default)
    {
        ThrowIfNotValid();

        ValidTo = validTo ?? DateTimeOffset.UtcNow;
    }

    private void ThrowIfNotValid()
    {
        if (ValidTo is not null)
        {
            throw new InvalidOperationException("");
        }
    }
}