using CosmosDbClient;
using Customer.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models
{
    /// <summary>
    /// The customer entity
    /// </summary>
    public class CustomerEntity : Entity
    {
        /// <summary>
        /// Default
        /// </summary>
        public CustomerEntity()
        {

        }

        /// <summary>
        /// Overloaded CTOR to allow creation of customer from request
        /// </summary>
        /// <param name="request">CreateCustomerRequest object</param>
        public CustomerEntity(CreateCustomerRequest request)
        {
            Email = request.Email;
        }

        /// <summary>
        /// Email address of the customer
        /// </summary>
        public string Email { get; set; }
    }
}
