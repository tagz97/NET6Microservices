using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions;
using ServiceCollectionExtensions.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerService(this IServiceCollection services)
        {
            services.AddService<ICustomerService, CustomerService>(ServiceType.SINGLETON);

            return services;
        }
    }
}
