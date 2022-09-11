using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.General
{
    public static class ItemResponseExtensions
    {
        public static double GetRequestCharge<T>(this ItemResponse<T> itemResponse)
        {
            return itemResponse.RequestCharge;
        }

        public static void LogDiagnostics<T,V>(this ItemResponse<T> itemResponse, ILogger<V> logger)
        {
            logger.LogInformation($"Activity Id: {itemResponse.ActivityId}" +
                $"Request status: {itemResponse.StatusCode}" +
                $"End to end Request time: {itemResponse.Diagnostics.GetClientElapsedTime()}\n" +
                $"Failed request count: {itemResponse.Diagnostics.GetFailedRequestCount()}\n" +
                $"Request Charge: {itemResponse.GetRequestCharge()}");
        }
    }
}
