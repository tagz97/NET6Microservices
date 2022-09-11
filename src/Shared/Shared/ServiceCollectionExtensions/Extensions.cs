using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions.Enum;

namespace ServiceCollectionExtensions
{
    /// <summary>
    /// Service collection extensions for a central location for adding services
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Inject a service in any type of pattern using the enum <see cref="ServiceType"/>
        /// </summary>
        /// <typeparam name="I">Interface</typeparam>
        /// <typeparam name="C">Class/Service/Implementation</typeparam>
        /// <param name="services">Services to add the injections to</param>
        /// <param name="serviceType" cref="ServiceType">Enum for injecting a service</param>
        /// <returns>Services with injected service added (<see cref="IServiceCollection"/>)</returns>
        /// <exception cref="ArgumentException">Service to add cannot be injected</exception>
        public static IServiceCollection AddService<I, C>(this IServiceCollection services, ServiceType serviceType)
            where I : class
            where C : class
        {
            if (typeof(I).IsAssignableFrom(typeof(C)))
            {
                switch (serviceType)
                {
                    case ServiceType.SINGLETON:
                        services.AddSingleton(typeof(I), typeof(C));
                        break;
                    case ServiceType.TRANSIENT:
                        services.AddTransient(typeof(I), typeof(C));
                        break;
                    case ServiceType.SCOPED:
                        services.AddScoped(typeof(I), typeof(C));
                        break;
                    default: throw new ArgumentException($"{nameof(ServiceType)} input is not valid");
                }
            }
            else
            {
                throw new ArgumentException($"{nameof(I)} is not assignable from {nameof(C)}");
            }

            return services;
        }
    }
}