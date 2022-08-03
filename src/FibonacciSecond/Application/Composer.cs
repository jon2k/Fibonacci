using Common.Contract;
using FibonacciSecond.Application.Command;
using MediatR;

namespace FibonacciSecond.Application;

public static class Composer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CalculateCommand, MessageResponseFib>, CalculateCommandHandler>();

        return services;
    }
}