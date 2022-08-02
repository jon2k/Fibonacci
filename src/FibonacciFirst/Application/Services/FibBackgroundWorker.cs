using Common.Contract;
using EasyNetQ;
using Fibonacci.Application.Interfaces;

namespace Fibonacci.Application.Services;

internal class FibBackgroundWorker : BackgroundService
{
    private readonly IBus _bus;
    private readonly IRepository _repository;
    private readonly ISenderService _senderService;

    public FibBackgroundWorker(IServiceProvider serviceProvider, IBus bus, IRepository repository)
    {
        _bus = bus;
        _repository = repository;
        _senderService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ISenderService>();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.PubSub.SubscribeAsync<MessageResponseFib>("test", Handler);

        return Task.CompletedTask;
    }

    public override void Dispose() // todo
    {
        _bus.Dispose();
        base.Dispose();
    }

    private async Task Handler(MessageResponseFib arg)
    {
        if (!_repository.HasNumber(arg.CurrentNumber)) // Exactly once
        {
            if (arg.TaskNumber >= arg.CurrentNumber)
            {
                _repository.AddFibNumber(arg.CurrentNumber, arg.Sum);

                await _senderService.SendAsync(arg.TaskNumber, CancellationToken.None);
            }
        }
    }
}