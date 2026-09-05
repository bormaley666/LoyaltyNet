using System.Threading.Tasks;

namespace LoyaltyNet.Web.Infrastructure;

public interface IStartupTask
{
    public Task ExecuteAsync();
}
