using Common;
using EasyNetQ;
using Fibonacci.Storage;

namespace Fibonacci.Services;

public class FibonacciBackgroundService : BackgroundService
{
    private readonly IBus _bus;
    private readonly IRepo _repo;
    private readonly IHttpClientService _httpClientService;

    public FibonacciBackgroundService(
        IServiceProvider serviceProvider, // todo так сделать везде
        IBus bus, 
        IRepo repo)
    {
        _bus = bus;
        _repo = repo;
        _httpClientService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IHttpClientService>();
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
        if (!_repo.HasNumber(arg.CurrentNumber))
        {
            if (arg.TaskNumber >= arg.CurrentNumber)
            {
                _repo.AddFibNumber(arg.CurrentNumber, arg.Sum);
                var oneFibNumberWithPrev = _repo.GetMaxSaveNumber();
                var message = new MessageRequestFib(
                    TaskNumber: arg.TaskNumber,
                    Number: oneFibNumberWithPrev.Number + 1,
                    Fib: oneFibNumberWithPrev.Fib,
                    PrevFib: oneFibNumberWithPrev.PrevFib);
                await _httpClientService.SendDataAsync(message, CancellationToken.None); // todo обработать результат
            }
        }
    }
}