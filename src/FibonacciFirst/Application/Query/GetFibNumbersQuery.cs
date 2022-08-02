using MediatR;

namespace Fibonacci.Application.Query;

internal sealed class GetFibNumbersQuery : IRequest<List<long>>
{
    public GetFibNumbersQuery(int number)
    {
        Number = number;
    }
    public int Number { get;  }
}