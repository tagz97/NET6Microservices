using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace CosmosPersistance.Client.Extensions.General
{
    /// <summary>
    /// Extensions for the <see cref="ItemResponse{T}"/> class
    /// </summary>
    public static class ItemResponseExtensions
    {
        /// <summary>
        /// Get the request charge of the <see cref="ItemResponse{T}"/>
        /// </summary>
        /// <typeparam name="T">The type of <see cref="ItemResponse{T}"/></typeparam>
        /// <param name="itemResponse">The item response</param>
        /// <returns><see cref="ItemResponse{T}.RequestCharge"/></returns>
        public static double GetRequestCharge<T>(this ItemResponse<T> itemResponse)
        {
            return itemResponse.RequestCharge;
        }

        /// <summary>
        /// Log diagnostics related to a database query
        /// </summary>
        /// <typeparam name="T"><see cref="ItemResponse{T}"/> type</typeparam>
        /// <typeparam name="V">Logger type</typeparam>
        /// <param name="itemResponse">The item response</param>
        /// <param name="logger">The logger instance</param>
        public static void LogDiagnostics<T,V>(this ItemResponse<T> itemResponse, ILogger<V> logger)
        {
            logger.LogInformation($"Activity Id: {itemResponse.ActivityId}" +
                $"Request status: {itemResponse.StatusCode}" +
                $"End to end Request time: {itemResponse.Diagnostics.GetClientElapsedTime()}\n" +
                $"Failed request count: {itemResponse.Diagnostics.GetFailedRequestCount()}\n" +
                $"Request Charge: {itemResponse.GetRequestCharge()}");
        }

        /// <summary>
        /// Determines whether a request has been successful or not
        /// </summary>
        /// <typeparam name="T">Type of <see cref="ItemResponse{T}"/></typeparam>
        /// <param name="itemResponse">The item response</param>
        /// <returns>Success state as boolean</returns>
        public static bool IsSuccessful<T>(this ItemResponse<T> itemResponse)
        {
            if (itemResponse.StatusCode != System.Net.HttpStatusCode.OK ||
                itemResponse.StatusCode != System.Net.HttpStatusCode.Created ||
                itemResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}