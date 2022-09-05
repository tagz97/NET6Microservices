using CosmosDbClient;
using Customer.Domain.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Customer.Repository
{
    /// <summary>
    /// Customer repository controlling all persistance for the customer API
    /// </summary>
    public class CustomerRepository : CosmosDbRepository<CustomerEntity> , ICustomerRepository
    {
        private readonly ICosmosDbClient _client;
        private readonly Container _container;

        /// <summary>
        /// Instantiate the repository from DI
        /// </summary>
        /// <param name="factory">Injected Cosmos factory</param>
        public CustomerRepository(ICosmosDbClientFactory factory) : base(factory)
        {
            _client = factory.GetClient("Customer");
            _container = _client.Container;
        }

        /// <summary>
        /// Name of Collection
        /// </summary>
        public override string CollectionName { get; } = "Customer";

        /// <summary>
        /// Resolve any conflict with partition key
        /// </summary>
        /// <param name="entityId">Id of the entity</param>
        /// <returns>PartitionKey</returns>
        public override PartitionKey? ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        /// <param name="customerEntity">Customer entity to persist</param>
        /// <returns>Persisted customer entity</returns>
        /// <exception cref="EntityAlreadyExistsException">Duplicate entity</exception>
        public async Task<CustomerEntity> AddCustomerAsync(CustomerEntity customerEntity)
        {
            customerEntity.Id = await GenerateUniqueIdentifier();
            ItemResponse<CustomerEntity> response = await _client.CreateDocumentAsync(customerEntity, ResolvePartitionKey(customerEntity.Id));
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new EntityAlreadyExistsException();
            }

            return response.Resource;
        }

        /// <summary>
        /// Check if customer email is unique in the system before creation
        /// </summary>
        /// <param name="email">Email to check uniqueness of</param>
        /// <returns>Whether the email is unique or not</returns>
        public async Task<bool> CheckCustomerEmailUniqueAsync(string email)
        {
            var customers = _client.ReadAllItemsAsIQueryable<CustomerEntity>();
            var filteredCustomers = await customers.Where(x => x.Email.ToLower() == email.ToLower()).CosmosToListAsync();

            return filteredCustomers.Any();
        }

        /// <summary>
        /// Get all customers in the database
        /// </summary>
        /// <returns>A list of customers</returns>
        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().CosmosToListAsync();
        }

        /// <summary>
        /// Get customer by their Id
        /// </summary>
        /// <param name="id">Id of the customer to get</param>
        /// <returns>Customer</returns>
        public async Task<CustomerEntity> GetCustomerByIdAsync(string id)
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().Where(x => x.Id == id).CosmosFirstOrDefaultAsync();
        }

        /// <summary>
        /// Get customers by email address
        /// </summary>
        /// <param name="email">Email address to search customers for</param>
        /// <returns>Customer</returns>
        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().Where(x => x.Email.ToLower().Contains(email.ToLower()) || x.Email.ToLower() == email.ToLower()).CosmosFirstOrDefaultAsync();
        }

        /// <summary>
        /// Generate a new GUID that is unique in the system so that customer documents will never conflict on create
        /// </summary>
        /// <returns>Unique Id</returns>
        private async Task<string> GenerateUniqueIdentifier()
        {
            var uniqueIdentifier = Guid.NewGuid().ToString();
            var resp = await GetCustomerByIdAsync(uniqueIdentifier);
            if (resp != null)
            {
                await GenerateUniqueIdentifier();
            }

            return uniqueIdentifier;
        } 
    }
}