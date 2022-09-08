using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions;
using ServiceCollectionExtensions.Enum;

namespace Customer.Service.Extensions
{
    /// <summary>
    /// Service extensions for DI
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the customer service using the service collection extensions to define type of implementation
        /// </summary>
        /// <param name="services">Services to add the injected service to</param>
        /// <returns>Services with CustomerService injected</returns>
        public static IServiceCollection AddCustomerService(this IServiceCollection services)
        {
            services.AddService<ICustomerService, CustomerService>(ServiceType.SINGLETON);

            return services;
        }
    }
}
