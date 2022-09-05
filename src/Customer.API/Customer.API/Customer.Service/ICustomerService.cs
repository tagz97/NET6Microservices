using Customer.Domain.Models;
using Customer.Domain.Models.Responses;
using Framework.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service
{
    public interface ICustomerService
    {
        Task<BaseResponse<CustomerEntity>> GetCustomerByIdAsync(string id);
        Task<CustomerResponse> CreateCustomerFromRequestAsync(HttpRequest request);
    }
}
