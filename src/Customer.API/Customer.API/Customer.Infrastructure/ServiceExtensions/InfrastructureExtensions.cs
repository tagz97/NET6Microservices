using CosmosDbClient;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions;
using ServiceCollectionExtensions.Enum;

namespace Customer.Repository.ServiceExtensions
{
    public static class InfrastructureExtensions
    {
        /// <summary>
        /// Add connection to a CosmosDb
        /// </summary>
        /// <param name="services">Services to add the cosmos connection to</param>
        /// <param name="uri">URI for the cosmos account</param>
        /// <param name="authKey">AuthKey for the cosmos account</param>
        /// <param name="databaseName">Database name to use (note, will be created if not found)</param>
        /// <param name="collectionNames">Dictionary<string,string> for containers names and relevant partition keys</param>
        /// <returns>Services with cosmos added</returns>
        public static IServiceCollection AddCosmosConnection(this IServiceCollection services, Uri uri, string authKey, string databaseName, Dictionary<string,string> collectionNames)
        {
            services.AddCosmosDb(uri, authKey, databaseName, collectionNames);

            return services;
        }

        /// <summary>
        /// Add customer repository to DI
        /// </summary>
        /// <param name="services">Services to add the customer repository to</param>
        /// <returns>Services with customer repository added</returns>
        public static IServiceCollection AddCustomerRepository(this IServiceCollection services)
        {
            services.AddService<ICustomerRepository, CustomerRepository>(ServiceType.SINGLETON);

            return services;
        }
    }
}
