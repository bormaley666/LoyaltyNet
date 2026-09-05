using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using LoyaltyNet.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoyaltyNet.Web.Infrastructure.Extensions;

public static class ModelStateDictionaryExtensions
{
    public static (string message, string code, int status, IReadOnlyList<ValidationError>? validationErrors) ExtractErrorDetails(this ModelStateDictionary modelState)
    {
        if (HasBodyError(modelState))
        {
            return ("Неверный формат запроса", "invalid_request", StatusCodes.Status400BadRequest, null);
        }

        var vaildationErrors = PrepareValidationErrors(modelState);

        return ("Неверный запрос", "invalid_request", StatusCodes.Status400BadRequest, vaildationErrors.Count != 0 ? vaildationErrors : null);
    }

    private static List<ValidationError> PrepareValidationErrors(ModelStateDictionary modelState)
    {
        var validationErrors = new List<ValidationError>();

        foreach (var kvp in modelState)
        {
            if (kvp.Value.Errors.Count == 0)
            {
                continue;
            }

            var error = kvp.Value.Errors[0];
            var (fieldName, errorMessage) = PrepareErrorMessage(kvp.Key, error.ErrorMessage, error.Exception);
            validationErrors.Add(new ValidationError(fieldName, errorMessage));
        }

        return validationErrors;
    }

    private static bool HasBodyError(ModelStateDictionary modelState)
    {
        foreach (var kvp in modelState)
        {
            var hasEmptyBodyError = kvp.Value.Errors.Any(e => e.ErrorMessage.Contains("non-empty", StringComparison.OrdinalIgnoreCase));

            if (kvp.Key == "$" || hasEmptyBodyError)
            {
                return true;
            }
        }

        return false;
    }

    private static (string fieldName, string errorMessage) PrepareErrorMessage(string fieldName, string errorMessage, Exception? exception)
    {
        if (exception is JsonException && fieldName.StartsWith("$."))
        {
            return (fieldName[2..], "Неверный формат или тип значения");
        }

        return (fieldName, errorMessage);
    }
}
