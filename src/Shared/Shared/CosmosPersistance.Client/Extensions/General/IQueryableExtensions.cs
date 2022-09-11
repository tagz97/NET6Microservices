using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosPersistance.Client.Extensions.General
{
    /// <summary>
    /// Extensions for IQueryable
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Return the first or default item
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="query">The existing instance of IQueryable</param>
        /// <returns>The first or default item</returns>
        public static async Task<T> FirstOrDefaultAsync<T>(this IQueryable<T> query)
        {
            if (query is null) return default;
            FeedIterator<T> iterator = query.ToFeedIterator();
            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync())
                {
                    if (!EqualityComparer<T>.Default.Equals(item, default))
                    {
                        iterator.Dispose();
                        return item;
                    }
                }
            }
            iterator.Dispose();

            return default;
        }

        /// <summary>
        /// Return a list of all items
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="query">The existing instance of IQueryable</param>
        /// <returns>A list of items</returns>
        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> query)
        {
            List<T> list = new();
            if (query == null) return list;
            FeedIterator<T> iterator = query.ToFeedIterator();
            while (iterator.HasMoreResults)
            {
                list.AddRange(await iterator.ReadNextAsync());
            }
            iterator.Dispose();

            return list;
        }
    }
}
