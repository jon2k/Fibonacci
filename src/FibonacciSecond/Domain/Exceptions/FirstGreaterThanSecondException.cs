namespace FibonacciSecond.Domain.Exceptions;

public class FirstGreaterThanSecondException: Exception
{
    public FirstGreaterThanSecondException(string message):base(message)
    {
        
    }
}