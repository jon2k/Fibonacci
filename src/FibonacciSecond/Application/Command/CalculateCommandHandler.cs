using Common.Contract;
using FibonacciSecond.Services;
using MediatR;

namespace FibonacciSecond.Application.Command;

internal sealed class CalculateCommandHandler: IRequestHandler<CalculateCommand, MessageResponseFib>
{
    private readonly IMessagesBus _messagesBus;
    private readonly ICalculateSum _calculateSum;
    private readonly ILogger<CalculateCommandHandler> _logger;

    public CalculateCommandHandler( IMessagesBus messagesBus, 
        ICalculateSum calculateSum,
        ILogger<CalculateCommandHandler> logger)
    {
        _messagesBus = messagesBus;
        _calculateSum = calculateSum;
        _logger = logger;
    }
    public async Task<MessageResponseFib> Handle(CalculateCommand request, CancellationToken cancellationToken)
    {
        var sum = _calculateSum.Sum(request.MessageRequestFib);
        await _messagesBus.SendMessageAsync(sum, cancellationToken);
      
        return sum;
    }
}