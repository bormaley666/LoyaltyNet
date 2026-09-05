using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.References.Mccs.Models.Requests;

/// <summary>
/// Запрос на обновление MCC
/// </summary>
public sealed class UpdateMccRequest
{
    private string _name = default!;

    /// <summary>
    /// Наименование MCC-кода
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    [MinLength(1)]
    [MaxLength(500)]
    public string Name
    {
        get => _name;
        set => _name = WebUtility.HtmlEncode(value);
    }
}
