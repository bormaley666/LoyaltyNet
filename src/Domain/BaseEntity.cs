using System;

namespace LoyaltyNet.Domain;

public abstract class BaseEntity : IAuditEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Дата и время создания сущности
    /// </summary>
    public DateTimeOffset CreatedAt { get; protected set; }

    /// <summary>
    /// Дата и время изменения сущности
    /// </summary>
    public DateTimeOffset UpdatedAt { get; protected set; }

    #region IAuditEntity
    DateTimeOffset IAuditEntity.CreatedAt
    {
        get => CreatedAt;
        set => CreatedAt = value;
    }

    DateTimeOffset IAuditEntity.UpdatedAt
    {
        get => UpdatedAt;
        set => UpdatedAt = value;
    } 
    #endregion
}