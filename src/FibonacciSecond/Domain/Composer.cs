using FibonacciSecond.Domain.Interfaces;

namespace FibonacciSecond.Domain;

public static class Composer
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICalculateSum, CalculateSum>();

        return services;
    }
}