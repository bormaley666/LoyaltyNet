using LoyaltyNet.Application;
using LoyaltyNet.Domain;
using LoyaltyNet.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class ServicesConfigurator : IApplicationConfigurator
{
    public int Order => 800;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();

        services.AddPersistenceServices(configuration);
        services.AddDomainServices();
        services.AddApplicationServices();
    }

    public void ConfigureMiddleware(IApplicationBuilder builder) { }
}
