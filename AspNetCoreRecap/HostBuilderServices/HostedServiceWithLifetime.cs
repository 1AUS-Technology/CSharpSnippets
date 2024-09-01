namespace AspNetCoreRecap.HostBuilderServices;

public class HostedServiceWithLifetime (IHostApplicationLifetime appLifetime, ILogger<HostedServiceWithLifetime> logger) : IHostedService, IHostedLifecycleService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application startAsync");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application stop async");
        return Task.CompletedTask;
    }

    public Task StartedAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application started");
        return Task.CompletedTask;
    }

    public Task StartingAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application starting");
        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application stopped");
        return Task.CompletedTask;
    }

    public Task StoppingAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Application stopping");
        return Task.CompletedTask;
    }
}