using CosmosDbClient;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions;
using ServiceCollectionExtensions.Enum;

namespace Customer.Repository.ServiceExtensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddCosmosConnection(this IServiceCollection services, Uri uri, string authKey, string databaseName, Dictionary<string,string> collectionNames)
        {
            services.AddCosmosDb(uri, authKey, databaseName, collectionNames);

            return services;
        }

        public static IServiceCollection AddCustomerRepository(this IServiceCollection services)
        {
            services.AddService<ICustomerRepository, CustomerRepository>(ServiceType.SINGLETON);

            return services;
        }
    }
}
