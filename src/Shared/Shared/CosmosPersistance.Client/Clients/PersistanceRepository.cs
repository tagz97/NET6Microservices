using CosmosPersistance.Client.Extensions.Exceptions;
using CosmosPersistance.Client.Extensions.General;
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
        public virtual async Task<string> GenerateId(T entity) {
            string id = Guid.NewGuid().ToString();
            try
            {
                var resp = await GetByIdAsync(id);
                return await GenerateId(entity);
            }
            catch(EntityNotFoundException ex)
            {
                return ex.Id;
            }
        }
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
            entity.Id = await GenerateId(entity);
            entity.DocumentCreatedUnix = GetCurrentUnixTime();
            ItemResponse<T> response = await cosmosPersistanceClient.CreateDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
            if (!response.IsSuccessful())
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
            if (response.Resource != null)
            {
                throw new EntityDeleteException($"Entity with id {entity.Id} failed to delete", entity.Id);
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
            ItemResponse<T> response = await cosmosPersistanceClient.PutDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
            if (response.StatusCode != HttpStatusCode.OK || response.StatusCode != HttpStatusCode.Created)
            {
                throw new EntityPutException<T>("Entity failed to be put", entity);
            }

            return response.Resource;
        }

        /// <inheritdoc />
        public async Task<T> UpdateAsync(T entity)
        {
            var cosmosPersistanceClient = _cosmosPersistanceClientFactory.GetPersistanceClient(CollectionName);
            entity.DocumentModifiedUnix = GetCurrentUnixTime();
            ItemResponse<T> response = await cosmosPersistanceClient.UpdateDocumentAsync<T>(entity, ResolvePartitionKey(entity.Id));
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
