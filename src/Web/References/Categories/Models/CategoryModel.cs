using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LoyaltyNet.Web.References.Mccs.Models;

namespace LoyaltyNet.Web.References.Categories.Models;

/// <summary>
/// Категория
/// </summary>
public sealed class CategoryModel
{
    /// <summary>
    /// Идентификатор категории
    /// </summary>
    [Required]
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// Наименование категории
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Описание категории
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Список MCC
    /// </summary>
    [Required]
    [JsonPropertyName("mcc")]
    public IList<MccModel> Mcc { get; set; } = default!;
}
