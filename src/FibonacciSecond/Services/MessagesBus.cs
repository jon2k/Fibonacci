using Common;
using EasyNetQ;
using FibonacciSecond.Contract;

namespace FibonacciSecond.Services;

internal class MessagesBus : IMessagesBus
{
    private readonly IBus _bus;
    private readonly ILogger<MessagesBus> _logger;

    public MessagesBus(IBus bus, ILogger<MessagesBus> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task SendMessageAsync(MessageResponseFib messageResponseFib, CancellationToken cancellationToken)
    {
        await _bus.PubSub.PublishAsync(messageResponseFib, cancellationToken); // todo нужно ли убивать его
        _logger.LogInformation($"Send into Messages Bus {messageResponseFib.TaskNumber}"); // todo логи к одному виду
    }
}