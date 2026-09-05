using LoyaltyNet.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class RoutingConfigurator : IApplicationConfigurator
{
    public int Order => 40;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration) { }

    public void ConfigureMiddleware(IApplicationBuilder builder)
    {
        builder.UseRouting();
    }
}
