using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.References.Mccs.Models;

/// <summary>
/// Параметры фильтрации MCC
/// </summary>
public sealed class MccFilterModel
{
    /// <summary>
    /// MCC или наименование
    /// </summary>
    [JsonPropertyName("query")]
    public string? Query { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("withCategory")]
    public bool? WithCategory { get; set; }
}