using Customer.Domain.Models;
using Customer.Repository;
using Framework.Enums;
using Framework.ResponseModel;
using Microsoft.Extensions.Logging;

namespace Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<CustomerEntity>> GetCustomerByIdAsync(string id)
        {
            BaseResponse<CustomerEntity> response = new();
            try
            {
                response.Data = new CustomerEntity()
                {
                    Email = "email",
                    Id = id
                };
                // response.Data = await _customerRepository.GetCustomerByIdAsync(id);
                response.ResponseCode = ResponseCode.No_Error;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetCustomerByIdAsync)} Exception raised.\n{ex.Message}");
                response.ResponseCode = ResponseCode.Get_Fail;
            }

            return response;
        }
    }
}