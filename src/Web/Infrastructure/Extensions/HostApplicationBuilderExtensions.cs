using System;
using System.Linq;
using LoyaltyNet.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyNet.Web.Infrastructure.Extensions;

public static class HostApplicationBuilderExtensions
{
    public static void ConfigureServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        var configurations = AssemblyScanner.GetImplementationsOf<IApplicationConfigurator>()
            .Select(startup => Activator.CreateInstance(startup) as IApplicationConfigurator)
            .Where(startup => startup is not null)
            .OrderBy(startup => startup!.Order);

        foreach (var configuration in configurations)
        {
            configuration!.ConfigureServices(services, builder.Configuration);
        }

        foreach (var startupTask in AssemblyScanner.GetImplementationsOf<IStartupTask>())
        {
            services.AddScoped(typeof(IStartupTask), startupTask);
        }
    }
}
