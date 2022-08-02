using Common;
using FibonacciSecond.Contract;

namespace FibonacciSecond.Services;

public interface IMessagesBus
{
    public Task SendMessageAsync(MessageResponseFib messageResponseFib, CancellationToken cancellationToken);
}