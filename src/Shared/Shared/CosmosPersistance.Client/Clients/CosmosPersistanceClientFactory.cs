using Azure;
using CosmosPersistance.Client.Interfaces;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Clients
{
    /// <inheritdoc />
    internal class CosmosPersistanceClientFactory : ICosmosPersistanceClientFactory
    {
        private readonly string _databaseName;
        private readonly Dictionary<string, string> _collectionNames;
        private readonly CosmosClient _cosmosClient;

        public CosmosPersistanceClientFactory(string databaseName, Dictionary<string, string> collectionNames, CosmosClient cosmosClient)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            _collectionNames = collectionNames ?? throw new ArgumentNullException(nameof(collectionNames));
            _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        }

        /// <inheritdoc />
        public ICosmosPersistanceClient GetPersistanceClient(string collectionName)
        {
            if (!_collectionNames.ContainsKey(collectionName))
            {
                throw new ArgumentException($"Unable to find collection: {collectionName}");
            }

            return new CosmosContainerClient(_cosmosClient.GetContainer(_databaseName, collectionName));
        }

        /// <summary>
        /// Ensure that the database is setup and all relevant containers are existing
        /// </summary>
        /// <param name="throughput">Autoscale Throughput to apply to the database</param>
        internal async Task SetupDbAsync(int throughput)
        {
            ThroughputProperties autoscaleThroughputProperties = ThroughputProperties.CreateAutoscaleThroughput(throughput);
            var response = await _cosmosClient.CreateDatabaseIfNotExistsAsync(id: _databaseName, throughputProperties: autoscaleThroughputProperties);
            foreach (var collectionName in _collectionNames)
            {
                ContainerProperties containerProperties = new()
                {
                    Id = collectionName.Key,
                    PartitionKeyPath = collectionName.Value,
                    ConflictResolutionPolicy = new()
                    {
                        Mode = ConflictResolutionMode.LastWriterWins,
                        ResolutionPath = "/_ts"
                    },
                    IndexingPolicy = new()
                    {
                        Automatic = false,
                        IndexingMode = IndexingMode.Lazy
                    }
                };
                await response.Database.CreateContainerIfNotExistsAsync(containerProperties);
            }
        }
    }
}
