using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Client
{
    public interface ICustomerClient
    {
        Task<T> GetCustomerById<T>(string id);
    }
}
