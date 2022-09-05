using Customer.Domain.Models;
using Customer.Domain.Models.Requests;
using Customer.Domain.Models.Responses;
using Customer.Repository;
using Customer.Service.Validators;
using Framework.Enums;
using Framework.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Validation;

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

        public async Task<CustomerResponse> CreateCustomerFromRequestAsync(HttpRequest request)
        {
            CustomerResponse response = new();
            var req = await request.GetBody<CreateCustomerRequest, CreateCustomerValidator>();

            if (!req.IsValid)
            {
                response.ResponseCode = ResponseCode.Create_Fail;
                _logger.LogError("Invalid request.", string.Join(",", req.Errors?.Select(e => e.ErrorMessage)));

                return response;
            }
            CustomerEntity customer = new(req.Value);
            response.Data = await _customerRepository.AddCustomerAsync(customer);

            return response;
        }

        public async Task<BaseResponse<CustomerEntity>> GetCustomerByIdAsync(string id)
        {
            BaseResponse<CustomerEntity> response = new();
            try
            {
                response.Data = await _customerRepository.GetCustomerByIdAsync(id);
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