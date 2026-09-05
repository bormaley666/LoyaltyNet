using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentResults;
using LoyaltyNet.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoyaltyNet.Web.Infrastructure;

public sealed class ProblemResponse
{
    private static readonly JsonSerializerOptions _defaultOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public static ProblemResponse Of(IError error)
    {
        ArgumentNullException.ThrowIfNull(error);

        var (message, code, status, details) = error.ExtractErrorDetails();

        return new ProblemResponse(
            status: status,
            message: message,
            code: code,
            details: details);
    }

    public static ProblemResponse Of(ModelStateDictionary modelState)
    {
        ArgumentNullException.ThrowIfNull(modelState);

        var (message, code, status, validationErrors) = modelState.ExtractErrorDetails();

        return new ProblemResponse(
            status: status,
            message: message,
            code: code,
            validationErrors: validationErrors);
    }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="status">HTTP статус-код ошибки</param>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="code">Код ошибки</param>
    /// <param name="validationErrors">Ошибки валидации запроса</param>
    /// <param name="details">Дополнительные сведения об ошибке</param>
    /// <param name="exception">Исключение</param>
    /// <exception cref="ArgumentNullException"></exception>
    [JsonConstructor]
    public ProblemResponse(
        int status,
        string message,
        string code,
        IReadOnlyList<ValidationError>? validationErrors = default,
        IReadOnlyDictionary<string, object>? details = default,
        Exception? exception = default)
    {
        Status = status;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        Code = code ?? throw new ArgumentNullException(nameof(code));
        ValidationErrors = validationErrors;
        Details = details;
        Exception = exception?.Demystify().ToString();
    }

    /// <summary>
    /// HTTP статус-код ошибки
    /// </summary>
    [JsonPropertyName("status")]
    public int Status { get; }

    /// <summary>
    /// Сообщение об ошибке
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; }

    /// <summary>
    /// Код ошибки
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; }

    /// <summary>
    /// Ошибки валидации запроса
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("validationErrors")]
    public IReadOnlyList<ValidationError>? ValidationErrors { get; }

    /// <summary>
    /// Дополнительные сведения об ошибке
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("details")]
    public IReadOnlyDictionary<string, object>? Details { get; }

    /// <summary>
    /// Информация об исключении
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("exception")]
    public string? Exception { get; }

    public string ToJson(JsonSerializerOptions? options = default)
    {
        return JsonSerializer.Serialize(this, options ?? _defaultOptions);
    }
}
