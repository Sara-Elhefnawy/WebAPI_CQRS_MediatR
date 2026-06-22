using Microsoft.Extensions.DependencyInjection;

namespace Gym.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ServiceCollection).Assembly;

        // Register all handlers (queries + commands) from this assembly.
        // MediatR scans for IRequestHandler<TRequest, TResponse> automatically.
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        return services;
    }
}
