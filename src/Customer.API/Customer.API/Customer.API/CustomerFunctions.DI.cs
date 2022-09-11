using Customer.Service;
using Microsoft.Extensions.Logging;

namespace Customer.API
{
    /// <summary>
    /// Partial class for <see cref="CustomerFunctions"/> to handle Dependency Injection
    /// </summary>
    public partial class CustomerFunctions
    {
        private readonly ILogger<CustomerFunctions> _logger;
        private readonly ICustomerService _customerService;

        /// <summary>
        /// CTOR for function with injected services
        /// </summary>
        /// <param name="log">Ilogger for logging</param>
        /// <param name="customerService"><see cref="ICustomerService"/> service for communication with other layers</param>
        public CustomerFunctions(ILogger<CustomerFunctions> log, ICustomerService customerService)
        {
            _logger = log;
            _customerService = customerService;
        }
    }
}
