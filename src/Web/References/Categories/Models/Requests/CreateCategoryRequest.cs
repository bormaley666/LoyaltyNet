using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using LoyaltyNet.Web.Infrastructure.Validation;

namespace LoyaltyNet.Web.References.Categories.Models.Requests;

/// <summary>
/// Создание новой категории
/// </summary>
public sealed class CreateCategoryRequest
{
    private string _name = default!;
    private string? _description;

    /// <summary>
    /// Код категории
    /// </summary>
    [JsonRequired]
    [CategoryCode]
    [JsonPropertyName("code")]
    public int Code { get; set; }

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
