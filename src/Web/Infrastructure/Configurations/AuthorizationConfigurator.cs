using LoyaltyNet.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class AuthorizationConfigurator : IApplicationConfigurator
{
    public int Order => 60;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
    }

    public void ConfigureMiddleware(IApplicationBuilder builder)
    {
        builder.UseAuthorization();
    }
}
