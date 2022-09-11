using CosmosPersistance.Client.Extensions.General;
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
    public class CosmosContainerClient : ICosmosPersistanceClient
    {
        private readonly Container _container;

        /// <inheritdoc />
        Container ICosmosPersistanceClient.Container => _container;

        /// <inheritdoc />
        public CosmosContainerClient(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(Container));
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> CreateDocumentAsync<T>(T document, PartitionKey? partitionKey)
        {
            return await _container.CreateItemAsync<T>(document, partitionKey);
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> DeleteDocumentAsync<T>(string documentId, PartitionKey partitionKey)
        {
            return await _container.DeleteItemAsync<T>(documentId, partitionKey);
        }

        /// <inheritdoc />
        public IQueryable<T> ReadAllItemsAsIQueryable<T>()
        {
            return _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> ReadAllItemsAsync<T>()
        {
            return await _container.GetItemLinqQueryable<T>(allowSynchronousQueryExecution: true).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> ReadDocumentAsync<T>(string documentId, PartitionKey partitionKey)
        {
            return await _container.ReadItemAsync<T>(documentId, partitionKey);
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> ReplaceDocumentAsync<T>(T document, string id, PartitionKey? partitionKey)
        {
            return await _container.ReplaceItemAsync(document, id, partitionKey);
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> PutDocumentAsync<T>(T document, PartitionKey? partitionKey)
        {
            return await _container.UpsertItemAsync(document, partitionKey);
        }

        /// <inheritdoc />
        public async Task<ItemResponse<T>> UpdateDocumentAsync<T>(T document, PartitionKey? partitionKey)
        {
            // replace to use patch operations
            return await _container.UpsertItemAsync<T>(document, partitionKey);
        }
    }
}
