using Common.Contract;
using EasyNetQ;
using Fibonacci.Application.Interfaces;

namespace Fibonacci.Infrastructure;

internal class FibConsumer : IFibConsumer<MessageResponseFib>
{
    private readonly IBus _bus;
    private readonly IRepository _repository;
    private readonly ISenderService _senderService;
    private readonly ILogger<FibConsumer> _logger;

    public FibConsumer(IServiceProvider serviceProvider, IBus bus, IRepository repository, ILogger<FibConsumer> logger)
    {
        _bus = bus;
        _repository = repository;
        _senderService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISenderService>();
        _logger = logger;
    }

    public async Task ConsumeAsync(MessageResponseFib message)
    {
        try
        {
            if (!_repository.HasNumber(message.CurrentNumber)) // Exactly once.
            {
                var lastFibNumber = _repository.GetLastFibNumber();

                if (message.CurrentNumber == lastFibNumber.Number + 1) // Bus message sequence broken.
                {
                    if (message.TaskNumber >= message.CurrentNumber)
                    {
                        _repository.AddFibNumber(message.CurrentNumber, message.Sum);

                        await _senderService.SendAsync(message.TaskNumber, CancellationToken.None);
                    }
                    else
                    {
                        _logger.LogInformation("Calculation for {TaskNumber} finished", message.TaskNumber);
                    }
                }
                else
                {
                    _logger.LogError("Lost messages from messageBus. Stop computing Fibonacci number for {TaskNumber}", message.TaskNumber);
                }
            }
            else
            {
                _logger.LogWarning("Repeated message received for {TaskNumber}", message.TaskNumber);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Stop computing Fibonacci number for {TaskNumber}",message.TaskNumber );
        }
    }

    public async Task SubscribeAsync(string subscriptionId, CancellationToken cancellationToken)
    {
        await _bus.PubSub.SubscribeAsync<MessageResponseFib>(subscriptionId, ConsumeAsync, cancellationToken);
    }

    public void Dispose()
    {
        _bus.Dispose();
    }
}