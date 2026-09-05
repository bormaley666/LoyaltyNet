using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure;

public interface IApplicationConfigurator
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);

    public void ConfigureMiddleware(IApplicationBuilder builder);

    public int Order { get; }
}
