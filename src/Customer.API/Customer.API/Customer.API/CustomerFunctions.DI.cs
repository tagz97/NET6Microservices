using Customer.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.API
{
    public partial class CustomerFunctions
    {
        private readonly ILogger<CustomerFunctions> _logger;
        private readonly ICustomerService _customerService;

        public CustomerFunctions(ILogger<CustomerFunctions> log, ICustomerService customerService)
        {
            _logger = log;
            _customerService = customerService;
        }
    }
}
