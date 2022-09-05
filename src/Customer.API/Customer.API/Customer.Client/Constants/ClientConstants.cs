using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Client.Constants
{
    internal class ClientConstants
    {
        private const string CustomerBaseUrl = "customer";
        public const string GetCustomerByIdentifier = CustomerBaseUrl;
        public const string CreateCustomer = CustomerBaseUrl;
        public const string UpdateCustomer = CustomerBaseUrl;
        public const string DeleteCustomer = CustomerBaseUrl;
    }
}
