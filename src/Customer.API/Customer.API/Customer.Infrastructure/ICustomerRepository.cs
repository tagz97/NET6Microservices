using Customer.Domain.Models;

namespace Customer.Repository
{
    public interface ICustomerRepository
    {
        Task<bool> CheckCustomerEmailUniqueAsync(string email);
        Task<CustomerEntity> AddCustomerAsync(CustomerEntity customerEntity);
        Task<CustomerEntity> GetCustomerByIdAsync(string id);
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);
        Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    }
}
