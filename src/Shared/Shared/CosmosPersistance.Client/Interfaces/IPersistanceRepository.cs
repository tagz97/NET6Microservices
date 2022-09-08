using CosmosPersistance.Client.Models;

namespace CosmosPersistance.Client.Interfaces
{
    /// <summary>
    /// The persistance repository
    /// </summary>
    /// <typeparam name="T">Type of entity</typeparam>
    public interface IPersistanceRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Get an item from the container/collection by its Id
        /// </summary>
        /// <param name="id">Id of the document to retrieve</param>
        /// <returns>The entity matching the Id</returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Add a new item to the container/collection
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Update an item in the container/collection
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>The updated entity</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Put a new item in the container/collection
        /// </summary>
        /// <param name="entity">Entity to put</param>
        /// <returns>The put entity</returns>
        Task<T> PutAsync(T entity);

        /// <summary>
        /// Delete an item from the container/collection
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Get all items in the container/collection
        /// </summary>
        /// <returns>IEnumerable with type T of all items</returns>
        Task<IEnumerable<T>> GetAllItemsAsync();
    }
}
