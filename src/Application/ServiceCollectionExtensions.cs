using LoyaltyNet.Application.References.Categories;
using LoyaltyNet.Application.References.Mccs;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMccQueryService, MccQueryService>();
        services.AddScoped<IMccCommandService, MccCommandService>();
        services.AddScoped<ICategoryQueryService, CategoryQueryService>();
        services.AddScoped<ICategoryCommandService, CategoryCommandService>();
    }
}
