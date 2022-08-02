using Common.Contract;

namespace FibonacciSecond.Services;

internal interface IMessagesBus
{
    public Task SendMessageAsync(MessageResponseFib messageResponseFib, CancellationToken cancellationToken);
}