namespace CosmosPersistance.Client.Interfaces
{
    /// <summary>
    /// The cosmos persistance client factory that allows the getting of a consumable client
    /// </summary>
    public interface ICosmosPersistanceClientFactory
    {
        /// <summary>
        /// Get the persistance client matching the provided string value
        /// </summary>
        /// <param name="collectionName">Name of the collection/container to connect to</param>
        /// <returns>A persistance client initialized for the provided collection/container</returns>
        ICosmosPersistanceClient GetPersistanceClient(string collectionName);
    }
}
