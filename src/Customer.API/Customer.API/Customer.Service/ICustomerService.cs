using Customer.Domain.Models;
using Customer.Domain.Models.Responses;
using Framework.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service
{
    /// <summary>
    /// Abstraction of the CustomerService
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get a customer by their Id
        /// </summary>
        /// <param name="id">Id of the customer to get</param>
        /// <returns>Customer response</returns>
        Task<CustomerResponse> GetCustomerByIdAsync(string id);
        /// <summary>
        /// Create customer from http request
        /// </summary>
        /// <param name="request">Http request to create customer from</param>
        /// <returns>Customer response</returns>
        Task<CustomerResponse> CreateCustomerFromRequestAsync(HttpRequest request);
        /// <summary>
        /// Update customer from http request
        /// </summary>
        /// <param name="request">Http request to update customer from</param>
        /// <returns>Customer response</returns>
        Task<CustomerResponse> UpdateCustomerFromRequestAsync(HttpRequest request);
        /// <summary>
        /// Delete the customer using their Id
        /// </summary>
        /// <param name="id">Id of the customer to delete</param>
        /// <returns>Customer response</returns>
        Task<CustomerResponse> DeleteCustomerAsync(string id);
    }
}
