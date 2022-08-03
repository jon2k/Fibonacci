using Common.Contract;
using EasyNetQ;
using FibonacciSecond.Application.Interfaces;

namespace FibonacciSecond.Infrastructure;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager builderConfiguration)
    {
        services.AddSingleton(RabbitHutch.CreateBus(builderConfiguration.GetConnectionString("RabbitMq")));
        services.AddSingleton<IFibProducer<MessageResponseFib>, FibProducer<MessageResponseFib>>();

        return services;
    }
}