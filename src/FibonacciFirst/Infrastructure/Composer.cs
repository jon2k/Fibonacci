using Common.Contract;
using EasyNetQ;
using Fibonacci.Application.Interfaces;
using Fibonacci.Infrastructure.Settings;

namespace Fibonacci.Infrastructure;

public static class Composer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.AddSingleton<IFibConsumer<MessageResponseFib>, FibConsumer>();
        services.AddSingleton<IRepository, Repository>();
        services.AddHttpClient();
        services.AddScoped<IHttpClientService, HttpClientService>();
        services.Configure<HttpClientSettings>(builderConfiguration.GetSection(nameof(HttpClientSettings)));
        services.AddSingleton(RabbitHutch.CreateBus(builderConfiguration.GetConnectionString("RabbitMq")));
        
        return services;
    }
}