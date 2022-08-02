using Fibonacci.Application.Command;
using Fibonacci.Application.Interfaces;
using Fibonacci.Application.Query;
using Fibonacci.Application.Services;
using MediatR;

namespace Fibonacci.Application;

public static class Composer
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<StartCalculateCommand, CalculateResult>, StartCalculateCommandHandler>();
        services.AddScoped<IRequestHandler<GetFibNumbersQuery, List<long>>, GetFibNumbersQueryHandler>();
        services.AddScoped<ISenderService, SenderService>();
        services.AddHostedService<FibBackgroundWorker>();
      
        return services;
    }
}