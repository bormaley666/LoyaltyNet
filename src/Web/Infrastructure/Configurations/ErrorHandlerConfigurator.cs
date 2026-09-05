using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class ErrorHandlerConfigurator : IApplicationConfigurator
{
    public int Order => 0;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<UnhandledExceptionHandler>();
    }

    public void ConfigureMiddleware(IApplicationBuilder builder)
    {
        builder.UseExceptionHandler();
    }
}
