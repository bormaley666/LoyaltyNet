using System.Collections.Generic;
using System.Linq;
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace LoyaltyNet.Web.Infrastructure.Extensions;

public static class FluentResultsExtensions
{
    public static (string message, string code, int status, IReadOnlyDictionary<string, object>? details) ExtractErrorDetails(this IError error)
    {
        var code = error.Metadata.GetValueOrDefault("code") as string;
        var status = error.Metadata.GetValueOrDefault("status") as int?;

        var details = error.Metadata
            .Where(kvp => kvp.Key is not "code" && kvp.Key is not "status")
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        return (error.Message ?? "Неверный запрос", code ?? "invalid_request",
            status ?? StatusCodes.Status400BadRequest, details.Count != 0 ? details : null);
    }
}
