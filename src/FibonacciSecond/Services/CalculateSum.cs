using Common;
using FibonacciSecond.Exceptions;

namespace FibonacciSecond.Services;

internal class CalculateSum : ICalculateSum
{
    private readonly ILogger<CalculateSum> _logger;

    public CalculateSum(ILogger<CalculateSum> logger)
    {
        _logger = logger;
    }

    public MessageResponseFib Sum(MessageRequestFib request)
    {
        try
        {
            if (request.Number < 0 || request.PrevFib < 0 || request.Fib < 0)
            {
                _logger.LogError("Number cannot be negative");
                throw new NegativeNumberException("Number cannot be negative");
            }

            if (request.PrevFib > request.Fib)
            {
                _logger.LogError("The first number cannot be greater than the second");
                throw new FirstGreaterThanSecondException("The first number cannot be greater than the second");
            }

            checked
            {
                var sum = request.PrevFib + request.Fib;
                return new MessageResponseFib(
                    TaskNumber: request.TaskNumber,
                    CurrentNumber: request.Number,
                    Sum: sum);
            }
        }
        catch (OverflowException e)
        {
            _logger.LogError(e, "Data type overflow occurred during addition");
            throw;
        }
    }
}