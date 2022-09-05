using Customer.Service;
using Microsoft.Extensions.Logging;

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
