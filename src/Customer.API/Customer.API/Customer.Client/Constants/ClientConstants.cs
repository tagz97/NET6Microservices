using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Client.Constants
{
    /// <summary>
    /// Client constants to be used for the customer client
    /// </summary>
    public class ClientConstants
    {
        private const string CustomerBaseUrl = "customer";
        /// <summary>
        /// Get customer by id base route
        /// </summary>
        public const string GetCustomerByIdentifier = CustomerBaseUrl;
        /// <summary>
        /// Create customer base route
        /// </summary>
        public const string CreateCustomer = CustomerBaseUrl;
        /// <summary>
        /// Update customer base route
        /// </summary>
        public const string UpdateCustomer = CustomerBaseUrl;
        /// <summary>
        /// Delete customer base route
        /// </summary>
        public const string DeleteCustomer = CustomerBaseUrl;
    }
}
