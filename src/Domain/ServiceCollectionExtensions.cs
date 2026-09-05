using LoyaltyNet.Domain.References.Categories.Services;
using LoyaltyNet.Domain.References.Mccs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Domain;

public static class ServiceCollectionExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IMccService, MccService>();
        services.AddScoped<ICategoryService, CategoryService>();
    }
}
