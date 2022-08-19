using System;
using Customer.Client.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Customer.Client.Extensions
{
    public static class CustomerClientExtensions
    {
        public static void AddCustomerClient(this IServiceCollection services, IConfiguration configuration)
        {
            string serviceBaseUrl = GetBaseUrlVariable(configuration);

            services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
            {
                client.BaseAddress = new Uri(serviceBaseUrl);
            });
        }

        private static string GetBaseUrlVariable(IConfiguration configuration)
        {
            return configuration["ServiceBaseUrl"] ?? throw new NullReferenceException($"{nameof(ClientConstants)} null reference for service base url");
        }
    }
}
