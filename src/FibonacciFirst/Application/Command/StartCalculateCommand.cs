using MediatR;

namespace Fibonacci.Application.Command;

public enum CalculateResult
{
    None,
    AlreadyCompleted,
    StartCalculate
}

internal sealed class StartCalculateCommand : IRequest<CalculateResult>
{
    public StartCalculateCommand(int number)
    {
        Number = number;
    }

    public int Number { get; }
}