namespace MultiReplica.Worker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var envName = Environment.GetEnvironmentVariable("INSTANCE_ID") ?? "**NOT INFORMED**";
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("[{env}] Worker running at: {time}", envName, DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
        logger.LogInformation("### Stop signal received...");
    }
}
