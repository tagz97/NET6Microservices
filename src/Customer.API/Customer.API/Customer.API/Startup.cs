using Customer.Repository.ServiceExtensions;
using Customer.Service.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

[assembly: FunctionsStartup(typeof(Customer.API.Startup))]
namespace Customer.API
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            AddServices(builder.Services, configuration);
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        /// <summary>
        /// Add relevant services for DI
        /// </summary>
        /// <param name="services">Services to add the DI to</param>
        /// <param name="config">Configuration for retrieving environment variables</param>
        /// <exception cref="ArgumentException">Missing value required for API to start</exception>
        private static void AddServices(IServiceCollection services, IConfiguration config)
        {
            var serviceEndpoint = new Uri(GetConfigValue(config, "Database:ServiceEndpoint"));
            var authKey = GetConfigValue(config, "Database:AuthKey") ?? throw new ArgumentException("Database AuthKey not found");
            var databaseName = GetConfigValue(config, "Database:DatabaseName") ?? throw new ArgumentException("Database DatabaseName not found");
            var collectionNames = new Dictionary<string, string>() {
                {
                    GetConfigValue(config, "Database:Customer:ContainerName") ?? throw new ArgumentException("Container name not found"),
                    GetConfigValue(config, "Database:Customer:PartitionKey") ?? throw new ArgumentException("Container partition key not found")
                }
            };

            services.AddLogging();

            services.AddCosmosConnection(serviceEndpoint, authKey, databaseName, collectionNames);
            services.AddCustomerRepository();
            services.AddCustomerService();
        }

        /// <summary>
        /// Get specific config value for provided string
        /// </summary>
        /// <param name="config">The config to get the value from</param>
        /// <param name="input">The name of the configuration input to get the value of</param>
        /// <returns>string for config value</returns>
        /// <exception cref="ArgumentException">Config not found therefore API cannot start</exception>
        private static string GetConfigValue(IConfiguration config, string input)
        {
            return config.GetValue<string>(input) ?? throw new ArgumentException($"Config with name {input} not found");
        }
    }
}
