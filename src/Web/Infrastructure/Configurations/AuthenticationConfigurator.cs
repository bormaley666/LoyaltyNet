using LoyaltyNet.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class AuthenticationConfigurator : IApplicationConfigurator
{
    public int Order => 50;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication();
    }

    public void ConfigureMiddleware(IApplicationBuilder builder)
    {
        builder.UseAuthentication();
    }
}
