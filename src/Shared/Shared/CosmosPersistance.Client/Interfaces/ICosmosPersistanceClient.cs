using Microsoft.Azure.Cosmos;

namespace CosmosPersistance.Client.Interfaces
{
    /// <summary>
    /// The persistance client interface to allow working with cosmos documents
    /// </summary>
    public interface ICosmosPersistanceClient
    {
        /// <summary>
        /// The current container
        /// </summary>
        Container Container { get; }

        /// <summary>
        /// Read a document from the container by its Id
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="documentId"></param>
        /// <param name="partitionKey"></param>
        /// <returns>CosmosItemResponse with type T</returns>
        Task<ItemResponse<T>> ReadDocumentAsync<T>(string documentId, PartitionKey partitionKey);

        /// <summary>
        /// Create a new document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document"></param>
        /// <param name="partitionKey"></param>
        /// <returns>CosmosItemResponse with type T</returns>
        Task<ItemResponse<T>> CreateDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Replace a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document"></param>
        /// <param name="partitionKey"></param>
        /// <returns>CosmosItemResponse with type T</returns>
        Task<ItemResponse<T>> ReplaceDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Update a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document"></param>
        /// <param name="partitionKey"></param>
        /// <returns>CosmosItemResponse with type T</returns>
        Task<ItemResponse<T>> UpdateDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Delete a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="documentId"></param>
        /// <param name="partitionKey"></param>
        /// <returns>CosmosItemResponse with type T</returns>
        Task<ItemResponse<T>> DeleteDocumentAsync<T>(string documentId, PartitionKey partitionKey);

        /// <summary>
        /// Read all items from the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns>A list of entities with type T</returns>
        Task<IEnumerable<T>> ReadAllItemsAsync<T>();

        /// <summary>
        /// Return an IQueryable of all items in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns>IQueryable of type T</returns>
        IQueryable<T> ReadAllItemsAsIQueryable<T>();
    }
}
