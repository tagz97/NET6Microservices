using CosmosPersistance.Client.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Generate a Guid
        /// </summary>
        /// <param name="entity">The entity to generate the Guid for</param>
        /// <returns>Guid as a string</returns>
        string GenerateId(T entity);

        /// <summary>
        /// Resolve partition key
        /// </summary>
        /// <param name="entityId">The partition key</param>
        /// <returns>Null or a PartitionKey</returns>
        PartitionKey? ResolvePartitionKey(string entityId);
    }
}
