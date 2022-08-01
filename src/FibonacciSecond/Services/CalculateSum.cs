using FibonacciSecond.Exceptions;
using FibonacciSecond.Request;

namespace FibonacciSecond.Services;

internal class CalculateSum : ICalculateSum
{
    private readonly ILogger<CalculateSum> _logger;

    public CalculateSum(ILogger<CalculateSum> logger)
    {
        _logger = logger;
    }

    public ResponseFib Sum(RequestFib requestFib)
    {
        try
        {
            if (requestFib.Number < 0 || requestFib.First < 0 || requestFib.Second < 0)
            {
                _logger.LogError("Number cannot be negative");
                throw new NegativeNumberException("Number cannot be negative");
            }

            if (requestFib.First > requestFib.Second)
            {
                _logger.LogError("The first number cannot be greater than the second");
                throw new FirstGreaterThanSecondException("The first number cannot be greater than the second");
            }

            checked
            {
                var sum = requestFib.First + requestFib.Second;
                return new ResponseFib(requestFib.Number, sum);
            }
        }
        catch (OverflowException e)
        {
            _logger.LogError(e, "Data type overflow occurred during addition");
            throw;
        }
    }
}