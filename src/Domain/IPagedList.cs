using System.Collections.Generic;

namespace LoyaltyNet.Domain;

/// <summary>
/// Постраничный список
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IPagedList<T>
{
    /// <summary>
    /// Индекс страницы
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Кол-во страниц
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Общее кол-во записей
    /// </summary>
    public int TotalItems { get; }

    /// <summary>
    /// Записи на странице
    /// </summary>
    public IList<T> Items { get; }
}
