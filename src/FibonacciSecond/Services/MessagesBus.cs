using EasyNetQ;
using FibonacciSecond.Request;

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

    public async Task SendMessageAsync(ResponseFib responseFib, CancellationToken cancellationToken)
    {
        await _bus.PubSub.PublishAsync(responseFib, "MyTopic", cancellationToken);
        _logger.LogInformation($"Send into Messages Bus {responseFib.Number}"); // todo логи к одному виду
    }
}