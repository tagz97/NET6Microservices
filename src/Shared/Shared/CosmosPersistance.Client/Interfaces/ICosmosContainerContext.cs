using CosmosPersistance.Client.Models;
using Microsoft.Azure.Cosmos;

namespace CosmosPersistance.Client.Interfaces
{
    /// <summary>
    /// ICosmosContainerContext to control the current client being used
    /// </summary>
    /// <typeparam name="T">Base entity to use</typeparam>
    public interface ICosmosContainerContext<in T> where T : EntityBase
    {
        /// <summary>
        /// Name of the collection/container
        /// </summary>
        string CollectionName { get; }
        /// <summary>
        /// Generate a unique GUID for the entity
        /// </summary>
        /// <param name="entity">The entity to generate the Guid for</param>
        /// <returns>Guid as a string</returns>
        Task<string> GenerateId(T entity);

        /// <summary>
        /// Resolve partition key
        /// </summary>
        /// <param name="entityId">The partition key</param>
        /// <returns>Null or a PartitionKey</returns>
        PartitionKey? ResolvePartitionKey(string entityId);
    }
}
