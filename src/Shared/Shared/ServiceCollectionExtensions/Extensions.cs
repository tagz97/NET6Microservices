using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions.Enum;

namespace ServiceCollectionExtensions
{
    public static class Extensions
    {
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