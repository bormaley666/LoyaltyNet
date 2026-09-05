using System;

namespace LoyaltyNet.Domain;

/// <summary>
/// Провайдер системного времени
/// </summary>
public interface ITimeProvider
{
    /// <summary>
    /// Возвращает текущее локальное время сервера
    /// </summary>
    /// <returns>Текущее локальное время с Kind = Local</returns>
    public DateTime GetLocalNow();

    /// <summary>
    /// Возвращает текущее локальное время сервера как UTC
    /// </summary>
    /// <returns>Текущее локальное время с Kind = Utc</returns>
    public DateTime GetLocalNowAsUtcKind();

    /// <summary>
    /// Возвращает текущее время сервера в формате UTC
    /// </summary>
    /// <returns>Текущее UTC время с Kind = Utc</returns>
    public DateTime GetUtcNow();
}
