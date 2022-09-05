using CosmosDbClient;
using Customer.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Domain.Models
{
    public class CustomerEntity : Entity
    {
        public CustomerEntity()
        {

        }

        public CustomerEntity(CreateCustomerRequest request)
        {
            Email = request.Email;
        }

        public string Email { get; set; }
    }
}
