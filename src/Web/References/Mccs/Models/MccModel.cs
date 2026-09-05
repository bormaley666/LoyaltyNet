using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.References.Mccs.Models;

/// <summary>
/// MCC
/// </summary>
public sealed class MccModel
{
    /// <summary>
    /// MCC-код
    /// </summary>
    [Required]
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    /// <summary>
    /// Наименование MCC
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
