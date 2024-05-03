using Org.Example.Service;

namespace ServiceProject1;

public class Worker 
    (
        ILogger<Worker> _logger,
        IBusinessService _service
    ) 
    : BackgroundService
{
   

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _service.Serve();
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }
}
