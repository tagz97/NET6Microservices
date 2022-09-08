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

        /// <summary>
        /// Initialise customer service with injected services
        /// </summary>
        /// <param name="customerRepository">Injected repository</param>
        /// <param name="logger">injected logger</param>
        public CustomerService(ICustomerRepository customerRepository, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Create customer from Http request
        /// </summary>
        /// <param name="request">Http request to create the customer from</param>
        /// <returns>Customer response</returns>
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

            var isUniqueEmail = await _customerRepository.CheckCustomerEmailUniqueAsync(customer.Email);

            if (!isUniqueEmail)
            {
                response.ResponseCode = ResponseCode.Create_Fail;
                _logger.LogError($"Email {customer.Email} already exists within the database.");

                return response;
            }

            response.Data = await _customerRepository.AddCustomerAsync(customer);

            return response;
        }

        public async Task<CustomerResponse> UpdateCustomerFromRequestAsync(HttpRequest request)
        {
            CustomerResponse response = new();
            try
            {
                var req = await request.GetBody<UpdateCustomerRequest, UpdateCustomerValidator>();
                if (!req.IsValid)
                {
                    response.ResponseCode = ResponseCode.Update_Fail;
                    _logger.LogError("Invalid request.", string.Join(",", req.Errors?.Select(e => e.ErrorMessage)));

                    return response;
                }

                CustomerEntity customer = await GetCustomerByCustomerIdAsync(req.Value.Id);
                if (customer == default || customer == null)
                {
                    response.ResponseCode = ResponseCode.Update_Fail;
                    _logger.LogError($"Customer with id {req.Value.Id} not found within the database.");

                    return response;
                }

                customer = new(req.Value);
                var isUniqueEmail = await _customerRepository.CheckCustomerEmailUniqueAsync(customer.Email);
                if (!isUniqueEmail)
                {
                    response.ResponseCode = ResponseCode.Update_Fail;
                    _logger.LogError($"Email {customer.Email} already exists within the database.");

                    return response;
                }

                response.Data = await _customerRepository.UpdateCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(UpdateCustomerFromRequestAsync)} failed to update customer with error:\n{ex.Message}");
                response.ResponseCode = ResponseCode.Update_Fail;
            }
            
            return response;
        }

        public async Task<CustomerResponse> DeleteCustomerAsync(string id)
        {
            CustomerResponse response = new();
            try
            {
                CustomerEntity customerToDelete = await _customerRepository.GetCustomerByIdAsync(id);
                if (customerToDelete == null || customerToDelete == default)
                {
                    response.ResponseCode = ResponseCode.Delete_Fail;
                }
                else
                {
                    var deletionResult = await _customerRepository.DeleteCustomerAsync(customerToDelete);
                    response.ResponseCode = deletionResult ? ResponseCode.No_Error : ResponseCode.Delete_Fail;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(DeleteCustomerAsync)} failed to delete customer with id {id} with error:\n{ex.Message}");
                response.ResponseCode = ResponseCode.Delete_Fail;
            }

            return response;
        }

        /// <summary>
        /// Get customer by their Id
        /// </summary>
        /// <param name="id">Id of the customer to get</param>
        /// <returns>Customer response</returns>
        public async Task<CustomerResponse> GetCustomerByIdAsync(string id)
        {
            CustomerResponse response = new();
            try
            {
                response.Data = await GetCustomerByCustomerIdAsync(id);
                response.ResponseCode = ResponseCode.No_Error;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(GetCustomerByIdAsync)} Exception raised.\n{ex.Message}");
                response.ResponseCode = ResponseCode.Get_Fail;
            }

            return response;
        }

        private async Task<CustomerEntity> GetCustomerByCustomerIdAsync(string id)
        {
            CustomerEntity customer;
            try
            {
                customer = await _customerRepository.GetCustomerByIdAsync(id);
            }
            catch (Exception)
            {
                throw;
            }

            return customer;
        }
    }
}