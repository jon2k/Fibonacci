namespace Fibonacci.Application.Interfaces;

internal interface IFibConsumer<in T> : IDisposable where T : class
{
    Task ConsumeAsync(T message);
    Task SubscribeAsync(string subscriptionId, CancellationToken cancellationToken);
}