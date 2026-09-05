using System;

namespace LoyaltyNet.Domain;

public interface IAuditEntity
{
    /// <summary>
    /// Дата и время создания сущности
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Дата и время изменения сущности
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}