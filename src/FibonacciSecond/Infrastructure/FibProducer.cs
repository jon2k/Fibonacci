using EasyNetQ;
using FibonacciSecond.Application.Interfaces;

namespace FibonacciSecond.Infrastructure;

internal class FibProducer<T> : IFibProducer<T> where T : class
{
    private readonly IBus _bus;
    private readonly ILogger<FibProducer<T>> _logger;

    public FibProducer(IBus bus, ILogger<FibProducer<T>> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    public async Task PublishAsync(T message, CancellationToken cancellationToken)
    {
        await _bus.PubSub.PublishAsync(message, cancellationToken);
        _logger.LogInformation("Send message");
    }

    public void Dispose()
    {
        _bus.Dispose();
    }
}