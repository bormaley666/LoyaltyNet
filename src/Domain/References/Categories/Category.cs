using System;
using System.Collections.Generic;
using System.Linq;
using LoyaltyNet.Domain.References.Mccs;

namespace LoyaltyNet.Domain.References.Categories;

/// <summary>
/// MCC-категория
/// </summary>
public sealed class Category : BaseEntity
{
    /// <summary>
    /// EF Core ctor
    /// </summary>
    private Category() { }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="code"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <param name="mcc"></param>
    /// <param name="validFrom"></param>
    public Category(CategoryCode code, string name, string? description, List<Mcc> mcc, DateTimeOffset validFrom)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Code = code;
        Name = name.Trim();
        Description = description?.Trim();
        Mcc = mcc;
        ValidFrom = validFrom;
    }

    /// <summary>
    /// Код категории
    /// </summary>
    public CategoryCode Code { get; private set; } = default!;

    /// <summary>
    /// Наименование категории
    /// </summary>
    public string Name { get; private set; } = default!;

    /// <summary>
    /// Описание категории
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTimeOffset ValidFrom { get; private set; }

    /// <summary>
    ///
    /// </summary>
    public DateTimeOffset? ValidTo { get; private set; }

    /// <summary>
    /// Список MCC в этой категории
    /// </summary>
    public IReadOnlyCollection<Mcc> Mcc { get; private set; } = default!;

    public bool IsEquivalentTo(string name, string? description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return Name == name.Trim() && Description == description?.Trim();
    }

    public bool Contains(Mcc mcc)
    {
        return Mcc.Any(m => m.Code == mcc.Code || m == mcc);
    }

    public void Deactivate(DateTimeOffset validTo)
    {
        ThrowIfNotValid();

        ValidTo = validTo;
    }

    private void ThrowIfNotValid()
    {
        if (ValidTo is not null)
        {
            throw new InvalidOperationException("");
        }
    }
}
