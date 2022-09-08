using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using CosmosPersistance.Client.Interfaces;
using CosmosPersistance.Client.Clients;

namespace CosmosPersistance.Client.Extensions.Service
{
    /// <summary>
    /// IServiceCollection extension that handles the adding of the cosmos persistance client
    /// </summary>
    public static class CosmosPersistanceServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the cosmos persistance client as a singleton to DI
        /// </summary>
        /// <param name="services">The IServiceCollection you wish to add the cosmos persistance client to</param>
        /// <param name="serviceEndpoint">The endpoint of your database</param>
        /// <param name="authKey">The authorization key of your database</param>
        /// <param name="databaseName">The name of the database</param>
        /// <param name="collectionNames">A Dictionary<string,string> for collection name and collection partition key respectively</param>
        /// <param name="throughput">The throughput to be applied to the database as Autoscale throughput</param>
        /// <returns>IServiceCollection with Cosmos Persistance Client injected as a singleton</returns>
        public static IServiceCollection AddCosmosPersistanceClient(this IServiceCollection services, Uri serviceEndpoint,
            string authKey, string databaseName, Dictionary<string, string> collectionNames, int throughput)
        {
            services.AddSingleton((s) =>
            {
                return SetupCosmosDatabase(serviceEndpoint, authKey, databaseName, collectionNames, throughput);
            });

            return services;
        }

        /// <summary>
        /// Create database and containers if not existing and return a consumable client
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint of your database</param>
        /// <param name="authKey">The authorization key of your database</param>
        /// <param name="databaseName">The name of the database</param>
        /// <param name="collectionNames">A Dictionary<string,string> for collection name and collection partition key respectively</param>
        /// <param name="throughput">The throughput to be applied to the database as Autoscale throughput</param>
        /// <returns>Returns an instance of the CosmosPersistanceClientFactory</returns>
        /// <exception cref="ArgumentException">Missing values that are required</exception>
        private static ICosmosPersistanceClientFactory SetupCosmosDatabase(Uri serviceEndpoint, string authKey, string databaseName, Dictionary<string, string> collectionNames, int databaseThroughput)
        {
            string endpoint = serviceEndpoint.ToString() ?? throw new ArgumentException("Please specify a valid ServiceEndpoint in your application configuration"); ;
            string key = authKey ?? throw new ArgumentException("Please specify a valid AuthKey in your application configuration");

            CosmosClientBuilder configurationBuilder = new(endpoint, key);
            CosmosClient cosmosClient = configurationBuilder.Build();

            var cosmosDbClientFactory = new CosmosPersistanceClientFactory(databaseName, collectionNames, cosmosClient);
            cosmosDbClientFactory.SetupDbAsync(databaseThroughput).Wait();

            return cosmosDbClientFactory;
        }
    }
}
