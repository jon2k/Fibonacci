using FibonacciSecond.Request;

namespace FibonacciSecond.Services;

public interface ICalculateSum
{
    public ResponseFib Sum(RequestFib requestFib);
}