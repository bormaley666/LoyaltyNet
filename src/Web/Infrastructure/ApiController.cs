using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LoyaltyNet.Web.Infrastructure;

[ApiController]
public abstract class ApiController : Controller
{
    public static ActionResult Problem(ModelStateDictionary modelState)
    {
        ArgumentNullException.ThrowIfNull(modelState);

        var problem = ProblemResponse.Of(modelState);

        return new ContentResult
        {
            StatusCode = problem.Status,
            Content = problem.ToJson(),
            ContentType = "application/json"
        };
    }

    public static ActionResult Problem(ResultBase result)
    {
        ArgumentNullException.ThrowIfNull(result);

        if (!result.IsFailed)
        {
            throw new ArgumentException("Result must be in failed state", nameof(result));
        }

        var problem = ProblemResponse.Of(result.Errors[0]);

        return new ContentResult
        {
            StatusCode = problem.Status,
            Content = problem.ToJson(),
            ContentType = "application/json"
        };
    }
}
