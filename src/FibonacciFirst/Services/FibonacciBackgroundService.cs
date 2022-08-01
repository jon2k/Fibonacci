using EasyNetQ;
using Fibonacci.Dto;

namespace Fibonacci.Services;

public class FibonacciBackgroundService :BackgroundService
{
    private readonly IBus _bus;
    private readonly IHttpClientFactory _httpClientFactory;

    public FibonacciBackgroundService(IBus bus, IHttpClientFactory httpClientFactory)
    {
        _bus = bus;
        _httpClientFactory = httpClientFactory;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.PubSub.SubscribeAsync<ResponseFib>("test", Handler);
        while (!stoppingToken.IsCancellationRequested)
        {
            if (Services.isWork)
            {
                for (int i = 0; i < Services.Number; i++)
                {

                }

                Services.isWork = false;
            }
        }
       
    }

    private async Task Handler(ResponseFib arg)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var answer= await httpClient.SendAsync();
    }
}