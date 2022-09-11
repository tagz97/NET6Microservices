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
        /// <param name="documentId">Id of the document</param>
        /// <param name="partitionKey">Partition Key</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> ReadDocumentAsync<T>(string documentId, PartitionKey partitionKey);

        /// <summary>
        /// Create a new document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document">The document to create</param>
        /// <param name="partitionKey">The partition key for the container</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> CreateDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Replace a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document">Document to replace</param>
        /// <param name="partitionKey">The partition key for the container</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> ReplaceDocumentAsync<T>(T document, string id, PartitionKey? partitionKey);

        /// <summary>
        /// Update a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document">Document to update</param>
        /// <param name="partitionKey">The partition key for the container</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> UpdateDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Put a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="document">Document to put</param>
        /// <param name="partitionKey">The partition key for the container</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> PutDocumentAsync<T>(T document, PartitionKey? partitionKey);

        /// <summary>
        /// Delete a document in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="documentId">Id of the document to delete</param>
        /// <param name="partitionKey">The partition key for the container</param>
        /// <returns><see cref="ItemResponse{T}"/></returns>
        Task<ItemResponse<T>> DeleteDocumentAsync<T>(string documentId, PartitionKey partitionKey);

        /// <summary>
        /// Read all items from the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> ReadAllItemsAsync<T>();

        /// <summary>
        /// Return an IQueryable of all items in the container
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns><see cref="IQueryable{T}"/></returns>
        IQueryable<T> ReadAllItemsAsIQueryable<T>();
    }
}
