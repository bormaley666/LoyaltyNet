using LoyaltyNet.Application;
using LoyaltyNet.Domain.References.Categories.Repositories;
using LoyaltyNet.Domain.References.Mccs.Repositories;
using LoyaltyNet.Persistence.References.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var migrationsAssembly = typeof(AppDbContext).Assembly.GetName().Name;

        services.AddDbContext<AppDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSnakeCaseNamingConvention()
                .UseNpgsql(connectionString, o => o.MigrationsAssembly(migrationsAssembly));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDatabaseMigrator, DatabaseMigrator>();

        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IMccQueryRepository, MccQueryRepository>();
        services.AddScoped<IMccCommandRepository, MccCommandRepository>();
        services.AddScoped<ICategoryQueryRepository, CategoryQueryRepository>();
        services.AddScoped<ICategoryCommandRepository, CategoryCommandRepository>();
    }
}
