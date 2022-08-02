using Fibonacci.Application.Interfaces;
using MediatR;

namespace Fibonacci.Application.Command;

internal sealed class StartCalculateCommandHandler : IRequestHandler<StartCalculateCommand, CalculateResult>
{
    private readonly IRepository _repository;
    private readonly ISenderService _senderService;
    private readonly ILogger<StartCalculateCommandHandler> _logger;

    public StartCalculateCommandHandler(
        IRepository repository,
        ISenderService senderService,
        ILogger<StartCalculateCommandHandler> logger)
    {
        _repository = repository;
        _senderService = senderService;
        _logger = logger;
    }

    public async Task<CalculateResult> Handle(StartCalculateCommand request, CancellationToken cancellationToken)
    {
        if (_repository.HasNumber(request.Number))
        {
            return CalculateResult.AlreadyCompleted;
        }

        await _senderService.SendAsync(request.Number, cancellationToken);
      
        return CalculateResult.StartCalculate;
    }
}