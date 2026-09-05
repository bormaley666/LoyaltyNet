using System.Text.Json.Serialization;

namespace LoyaltyNet.Web.Infrastructure;

/// <summary>
/// Представляет ошибку валидации для поля запроса
/// </summary>
public sealed class ValidationError
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="field">Наименование поля, к которому относится ошибка валидации</param>
    /// <param name="message">Сообщение об ошибке валидации, описывающее причину отклонения значения</param>
    [JsonConstructor]
    public ValidationError(string field, string message)
    {
        Field = field ?? "";
        Message = message ?? "";
    }

    /// <summary>
    /// Наименование поля, к которому относится ошибка валидации
    /// </summary>
    [JsonPropertyName("field")]
    public string Field { get; set; }

    /// <summary>
    /// Сообщение об ошибке валидации, описывающее причину отклонения значения
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; }
}
