using System;
using LoyaltyNet.Infrastructure.Abstractions.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Infrastructure.Caching;

public static class ServiceCollectionExtensions
{
    private const string DefaultConfigurationSectionName = "Caching";

    public static IServiceCollection AddCaching(this IServiceCollection services, string? configurationSectionName = default)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ICacheManager, CacheManager>(sp => 
        {
            var configuration = sp.GetService<IConfiguration>();

            var options = configuration?.GetSection(configurationSectionName ?? DefaultConfigurationSectionName)
                .Get<CacheOptions>();

            return new CacheManager(options ?? new CacheOptions());
        });

        return services;
    }
}