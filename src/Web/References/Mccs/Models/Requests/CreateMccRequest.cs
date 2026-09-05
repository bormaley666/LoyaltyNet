using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LoyaltyNet.Web.Infrastructure.Validation;

namespace LoyaltyNet.Web.References.Mccs.Models.Requests;

public sealed class CreateMccRequest
{
    [Required]
    [MccCode]
    [JsonPropertyName("code")]
    public string Code { get; set; } = default!;

    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}
