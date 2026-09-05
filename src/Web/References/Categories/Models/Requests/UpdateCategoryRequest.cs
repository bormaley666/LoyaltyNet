using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.References.Categories.Models.Requests;

/// <summary>
/// Обновление категории
/// </summary>
public sealed class UpdateCategoryRequest
{
    private string _name = default!;
    private string? _description;

    /// <summary>
    /// Наименование категории
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    [MinLength(1)]
    [MaxLength(100)]
    public string Name
    {
        get => _name;
        set => _name = WebUtility.HtmlEncode(value);
    }

    /// <summary>
    /// Описание категории
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("description")]
    [MaxLength(250)]
    public string? Description
    {
        get => _description;
        set => _description = WebUtility.HtmlEncode(value);
    }
}
