namespace Fibonacci.Application.Interfaces;

internal interface ISenderService
{
    Task SendAsync(int number, CancellationToken cancellationToken);
}