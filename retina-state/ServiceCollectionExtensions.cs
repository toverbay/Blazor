using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Linq;
using MediatR;
using RetinaState.Behaviors.CloneState;
using RetinaState.Behaviors.ReduxDevTools;
using RetinaState.Features.JavaScriptInterop;
using RetinaState.Features.Routing;

namespace RetinaState
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRetinaState(
            this IServiceCollection services,
            Action<Options> configure)
        {
            var options = new Options();

            configure?.Invoke(options);

            ServiceDescriptor loggerServiceDescriptor =
                services.FirstOrDefault(s => s.ServiceType == typeof(ILogger<>));

            if (loggerServiceDescriptor == null)
            {
                services.AddSingleton(typeof(ILogger<>), typeof(NullLogger<>));
            }

            services.AddMediatR();

            if (options.UseCloneStateBehavior)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CloneStateBehavior<,>));
                
            }

            if (options.UseReduxDevToolsBehavior)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ReduxDevToolsBehavior<,>));
                services.AddSingleton<ReduxDevToolsInterop>();
                services.AddSingleton<JsonRequestHandler>();
                services.AddSingleton<ComponentRegistry>();
            }

            if (options.UseRouting)
            {
                services.AddSingleton<RouteManager>();
            }

            return services;
        }
    }

    public class Options
    {
        public bool UseCloneStateBehavior { get; set; } = true;
        public bool UseReduxDevToolsBehavior { get; set; } = true;
        public bool UseRouting { get; set; } = true;

    }
}
