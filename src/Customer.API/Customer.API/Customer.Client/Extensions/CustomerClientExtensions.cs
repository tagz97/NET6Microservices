using System;
using Customer.Client.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Client.Extensions
{
    public static class CustomerClientExtensions
    {
        /// <summary>
        /// Add customer client to DI to allow consuming. Requires config of "ServiceBaseUrl" to exist.
        /// </summary>
        /// <param name="services">IServiceCollection to add the customer client to</param>
        /// <param name="configuration">Configuration to allow client to be initialised</param>
        public static IServiceCollection AddCustomerClient(this IServiceCollection services, IConfiguration configuration)
        {
            string serviceBaseUrl = GetBaseUrlVariable(configuration);

            services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
            {
                client.BaseAddress = new Uri(serviceBaseUrl);
            });

            return services;
        }

        /// <summary>
        /// Get base url variable from coonfiguration
        /// </summary>
        /// <param name="configuration">Configuration for function</param>
        /// <returns>Base URL variable from configuration</returns>
        /// <exception cref="NullReferenceException">Null reference when configuration value is null</exception>
        private static string GetBaseUrlVariable(IConfiguration configuration)
        {
            return configuration["ServiceBaseUrl"] ?? throw new NullReferenceException($"{nameof(ClientConstants)} null reference for service base url");
        }
    }
}
