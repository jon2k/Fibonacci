using Common.Contract;
using Fibonacci.Application.Interfaces;

namespace Fibonacci.Application.Services;

internal class SenderService : ISenderService
{
    private readonly IHttpClientService _httpClientService;
    private readonly IRepository _repository;
    private readonly ILogger<SenderService> _logger;

    public SenderService(IHttpClientService httpClientService, IRepository repository, ILogger<SenderService> logger)
    {
        _httpClientService = httpClientService;
        _repository = repository;
        _logger = logger;
    }

    public async Task SendAsync(int number, CancellationToken cancellationToken)
    {
        var oneFibNumberWithPrev = _repository.GetMaxSaveNumber();

        var message = new MessageRequestFib(
            TaskNumber: number,
            Number: oneFibNumberWithPrev.Number + 1,
            Fib: oneFibNumberWithPrev.Fib,
            PrevFib: oneFibNumberWithPrev.PrevFib);

        var result = await _httpClientService.SendDataAsync(message, cancellationToken);
        _logger.LogInformation(result.ToString());
    }
}