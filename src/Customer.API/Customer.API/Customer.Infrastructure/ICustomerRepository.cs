using Customer.Domain.Models;

namespace Customer.Repository
{
    /// <summary>
    /// Abstraction of the CustomerRepository that controls all persistance in Customer.API
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Check if the customer email is unique
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>Whether the customer email is unique</returns>
        Task<bool> CheckCustomerEmailUniqueAsync(string email);
        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="customerEntity">Customer to add</param>
        /// <returns>Added customer</returns>
        Task<CustomerEntity> AddCustomerAsync(CustomerEntity customerEntity);
        /// <summary>
        /// Get a customer by their Id
        /// </summary>
        /// <param name="id">Id of the customer</param>
        /// <returns>Customer that matches the Id</returns>
        Task<CustomerEntity> GetCustomerByIdAsync(string id);
        /// <summary>
        /// Get customer by email address
        /// </summary>
        /// <param name="email">Email address of the customer</param>
        /// <returns>Customer</returns>
        Task<CustomerEntity> GetCustomerByEmailAsync(string email);
        /// <summary>
        /// Get all customers in the database
        /// </summary>
        /// <returns>A list of all customers</returns>
        Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
        /// <summary>
        /// Delete customer using existing customer record
        /// </summary>
        /// <param name="customer">Customer to delete</param>
        /// <returns>Whether deletion success or not</returns>
        Task<bool> DeleteCustomerAsync(CustomerEntity customer);
        /// <summary>
        /// Update a customer document in the database
        /// </summary>
        /// <param name="customerEntity">Customer entity to update</param>
        /// <returns>Updated customer entity</returns>
        Task<CustomerEntity> UpdateCustomerAsync(CustomerEntity customerEntity);
    }
}
