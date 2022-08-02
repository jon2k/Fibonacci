using Common.Contract;

namespace FibonacciSecond.Services;

public interface ICalculateSum
{
    public MessageResponseFib Sum(MessageRequestFib request);
}