using System;

namespace LoyaltyNet.Domain;

public interface ISoftDeleteEntity
{
    /// <summary>
    /// Дата и время удаления
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    /// Признак удаленной записи
    /// </summary>
    public bool IsDeleted { get; set; }
}
