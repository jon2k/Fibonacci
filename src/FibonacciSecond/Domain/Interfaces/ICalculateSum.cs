using Common.Contract;

namespace FibonacciSecond.Domain.Interfaces;

public interface ICalculateSum
{
    public MessageResponseFib Sum(MessageRequestFib request);
}