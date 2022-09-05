using Customer.Domain.Models;
using Framework.ResponseModel;
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
    }
}
