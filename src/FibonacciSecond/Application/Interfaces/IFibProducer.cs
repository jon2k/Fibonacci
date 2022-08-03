namespace FibonacciSecond.Application.Interfaces;

internal interface IFibProducer<in T> :IDisposable where T : class
{
    Task PublishAsync(T message, CancellationToken cancellationToken);
}