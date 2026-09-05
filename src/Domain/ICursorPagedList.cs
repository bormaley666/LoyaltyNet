using System.Collections.Generic;

namespace LoyaltyNet.Domain;

/// <summary>
/// Постраничный список
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICursorPagedList<T>
{
    /// <summary>
    /// Маркер следующей страницы
    /// </summary>
    public string? NextPageToken { get; }

    /// <summary>
    /// Записи на странице
    /// </summary>
    public IList<T> Items { get; }
}
