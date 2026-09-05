using Microsoft.AspNetCore.Mvc.Filters;

namespace LoyaltyNet.Web.Infrastructure.Attributes;

public class NoCacheAttribute : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
        context.HttpContext.Response.Headers.Pragma = "no-cache";
        context.HttpContext.Response.Headers.Expires = "0";

        base.OnResultExecuting(context);
    }
}
