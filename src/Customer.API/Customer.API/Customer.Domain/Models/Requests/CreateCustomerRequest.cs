using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models.Requests
{
    /// <summary>
    /// Create customer request object. Used for creation of new customers
    /// </summary>
    public class CreateCustomerRequest
    {
        /// <summary>
        /// Email of the customer
        /// </summary>
        public string Email { get; set; }
    }
}
