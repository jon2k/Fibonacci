using Common;
using FibonacciSecond.Contract;

namespace FibonacciSecond.Services;

public interface ICalculateSum
{
    public MessageResponseFib Sum(MessageRequestFib request);
}