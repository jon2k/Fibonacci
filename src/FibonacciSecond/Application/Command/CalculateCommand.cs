using Common.Contract;
using MediatR;

namespace FibonacciSecond.Application.Command;

internal sealed class CalculateCommand: IRequest<MessageResponseFib>
{
    public CalculateCommand(MessageRequestFib messageRequestFib)
    {
        MessageRequestFib = messageRequestFib;
    }
    public MessageRequestFib MessageRequestFib { get;  }
}