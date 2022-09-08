using CosmosPersistance.Client.Extensions.Exceptions;
using CosmosPersistance.Client.Interfaces;
using CosmosPersistance.Client.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CosmosPersistance.Client.Clients
{
    /// <inheritdoc />
    public abstract class PersistanceRepository<T> : IPersistanceRepository<T>, ICosmosContainerContext<T> where T : EntityBase
    {
        private readonly ICosmosPersistanceClientFactory _cosmosPersistanceClientFactory;
        /// <inheritdoc />
        public abstract string CollectionName { get; }
        /// <inheritdoc />
        public virtual string GenerateId(T entity) => Guid.NewGuid().ToString();
        /// <inheritdoc />
        public virtual PartitionKey? ResolvePartitionKey(string entityId) => null;

        protected PersistanceRepository(ICosmosPersistanceClientFactory cosmosPersistanceClientFactory)
        {
            _cosmosPersistanceClientFactory = cosmosPersistanceClientFactory;
        }

        /// <inheritdoc />
        public async Task<T> AddAsync(T entity)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            entity.Id = GenerateId(entity);
            entity.DocumentCreatedUnix = GetCurrentUnixTime();
            ItemResponse<T> response = await cosmosPersistanceClient.CreateDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new EntityAlreadyExistsException($"Entity with id {entity.Id} not found", entity.Id);
            }

            return response.Resource;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(T entity)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            ItemResponse<T> response = await cosmosPersistanceClient.DeleteDocumentAsync<T>(entity.Id, ResolvePartitionKey(entity.Id).Value);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException();
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            return await cosmosPersistanceClient.ReadAllItemsAsync<T>();
        }

        /// <inheritdoc />
        public async Task<T> GetByIdAsync(string id)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            ItemResponse<T> response = await cosmosPersistanceClient.ReadDocumentAsync<T>(id, ResolvePartitionKey(id).Value);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException($"Entity with id {id} not found", id);
            }

            return response.Resource;
        }

        /// <inheritdoc />
        public async Task<T> PutAsync(T entity)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            entity.DocumentCreatedUnix = GetCurrentUnixTime();
            entity.DocumentModifiedUnix = GetCurrentUnixTime();
            ItemResponse<T> response = await cosmosPersistanceClient.ReplaceDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException($"Entity with id {entity.Id} not found", entity.Id);
            }

            return response.Resource;
        }

        /// <inheritdoc />
        public async Task<T> UpdateAsync(T entity)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            entity.DocumentModifiedUnix = GetCurrentUnixTime();
            ItemResponse<T> response = await cosmosPersistanceClient.ReplaceDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new EntityNotFoundException($"Entity with id {entity.Id} not found", entity.Id);
            }

            return response.Resource;
        }

        /// <summary>
        /// Get the current unix epoch ticks
        /// </summary>
        /// <returns>The current UnixEpoch ticks as a long</returns>
        private long GetCurrentUnixTime() { return DateTime.UnixEpoch.Ticks; }
    }
}
