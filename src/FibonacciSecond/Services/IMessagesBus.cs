using FibonacciSecond.Request;

namespace FibonacciSecond.Services;

public interface IMessagesBus
{
    public Task SendMessageAsync(ResponseFib responseFib, CancellationToken cancellationToken);
}