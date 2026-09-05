using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoyaltyNet.Web.Infrastructure;

public class StartupHostedService(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken token)
    {
        using var scope = _serviceProvider.CreateScope();

        foreach (var startupTask in scope.ServiceProvider.GetServices<IStartupTask>())
        {
            await startupTask.ExecuteAsync();
        }
    }

    public Task StopAsync(CancellationToken token)
    {
        return Task.CompletedTask;
    }
}
