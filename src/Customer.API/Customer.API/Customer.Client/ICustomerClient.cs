using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Client
{
    /// <summary>
    /// Abstraction of the CustomerClient for consumption
    /// </summary>
    public interface ICustomerClient
    {
        /// <summary>
        /// Get customer by ID
        /// </summary>
        /// <typeparam name="T">Response format</typeparam>
        /// <param name="id">ID of the customer to return</param>
        /// <returns>Customer response object</returns>
        Task<T> GetCustomerById<T>(string id);
    }
}
