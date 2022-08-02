using Common.Contract;
using EasyNetQ;
using FibonacciSecond.Services;

namespace FibonacciSecond.Infrastructure;

internal class MessagesBus : IMessagesBus // todo readme
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
        Console.WriteLine(_bus.GetHashCode());
        await _bus.PubSub.PublishAsync(messageResponseFib, cancellationToken); // todo нужно ли убивать его
        
        _logger.LogInformation($"Send into Messages Bus {messageResponseFib.TaskNumber}");
    }
}