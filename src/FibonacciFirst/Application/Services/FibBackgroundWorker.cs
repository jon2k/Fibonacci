using Common.Contract;
using Fibonacci.Application.Interfaces;

namespace Fibonacci.Application.Services;

internal class FibBackgroundWorker : BackgroundService
{
    private readonly ILogger<FibBackgroundWorker> _logger;
    private readonly IFibConsumer<MessageResponseFib> _fibConsumer;

    public FibBackgroundWorker(IFibConsumer<MessageResponseFib> fibConsumer, ILogger<FibBackgroundWorker> logger)
    {
        _fibConsumer = fibConsumer;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _fibConsumer.SubscribeAsync("test", stoppingToken);
        _logger.LogInformation("Consumer subscribed to events");

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _fibConsumer.Dispose();
        base.Dispose();
    }
}