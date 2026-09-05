using System.Threading.Tasks;

namespace LoyaltyNet.Persistence;

public interface IDatabaseMigrator
{
    public Task ApplyMigrationsAsync();
}