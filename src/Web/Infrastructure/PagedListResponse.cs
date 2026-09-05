using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.Infrastructure;

/// <summary>
/// Постраничный список
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedListResponse<T>
{
    /// <summary>
    /// Индекс страницы
    /// </summary>
    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    /// <summary>
    /// Размер страницы
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    /// <summary>
    /// Кол-во страниц
    /// </summary>
    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    /// <summary>
    /// Общее кол-во записей
    /// </summary>
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }

    /// <summary>
    /// Записи на странице
    /// </summary>
    [JsonPropertyName("items")]
    public IEnumerable<T> Items { get; set; } = [];
}
