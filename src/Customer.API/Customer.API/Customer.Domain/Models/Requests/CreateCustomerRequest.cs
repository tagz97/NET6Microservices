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

        /// <summary>
        /// First name of the customer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name / Surname of the customer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Mobile number of the customer
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// PostCode for the customer
        /// </summary>
        public string PostCode { get; set; }
    }
}
