using Common.Contract;
using FibonacciSecond.Application.Interfaces;
using FibonacciSecond.Domain.Interfaces;
using MediatR;

namespace FibonacciSecond.Application.Command;

internal sealed class CalculateCommandHandler : IRequestHandler<CalculateCommand, MessageResponseFib>
{
    private readonly IFibProducer<MessageResponseFib> _producer;
    private readonly ICalculateSum _calculateSum;
    private readonly ILogger<CalculateCommandHandler> _logger;

    public CalculateCommandHandler(IFibProducer<MessageResponseFib> producer,
        ICalculateSum calculateSum,
        ILogger<CalculateCommandHandler> logger)
    {
        _producer = producer;
        _calculateSum = calculateSum;
        _logger = logger;
    }

    public async Task<MessageResponseFib> Handle(CalculateCommand request, CancellationToken cancellationToken)
    {
        var sum = _calculateSum.Sum(request.MessageRequestFib);
        await _producer.PublishAsync(sum, cancellationToken);

        return sum;
    }
}