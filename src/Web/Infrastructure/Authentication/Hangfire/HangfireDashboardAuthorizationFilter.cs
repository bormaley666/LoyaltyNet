using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace LoyaltyNet.Web.Infrastructure.Authentication.Hangfire;

public sealed class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}
