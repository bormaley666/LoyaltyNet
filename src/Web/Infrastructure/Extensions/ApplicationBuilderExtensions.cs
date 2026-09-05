using System;
using System.Linq;
using LoyaltyNet.Domain;
using Microsoft.AspNetCore.Builder;

namespace LoyaltyNet.Web.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureMiddlewares(this IApplicationBuilder builder)
    {
        var startupConfigurations = AssemblyScanner.GetImplementationsOf<IApplicationConfigurator>();

        var instances = startupConfigurations
            .Select(startup => Activator.CreateInstance(startup) as IApplicationConfigurator)
            .Where(startup => startup is not null)
            .OrderBy(startup => startup!.Order);

        foreach (var instance in instances)
        {
            instance!.ConfigureMiddleware(builder);
        }
    }
}
