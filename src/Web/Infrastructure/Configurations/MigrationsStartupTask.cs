using System.Threading.Tasks;
using LoyaltyNet.Persistence;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public class MigrationsStartupTask(IDatabaseMigrator migrator) : IStartupTask
{
    private readonly IDatabaseMigrator _migrator = migrator;

    public async Task ExecuteAsync()
    {
        await _migrator.ApplyMigrationsAsync();
    }
}
