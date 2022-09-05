using CosmosDbClient;
using Customer.Domain.Models;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace Customer.Repository
{
    public class CustomerRepository : CosmosDbRepository<CustomerEntity> , ICustomerRepository
    {
        private readonly ICosmosDbClient _client;
        private readonly Container _container;

        public CustomerRepository(ICosmosDbClientFactory factory) : base(factory)
        {
            _client = factory.GetClient("Customer");
            _container = _client.Container;
        }

        public override string CollectionName { get; } = "Customer";

        public override PartitionKey? ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

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

        public async Task<bool> CheckCustomerEmailUniqueAsync(string email)
        {
            var customers = _client.ReadAllItemsAsIQueryable<CustomerEntity>();
            var filteredCustomers = await customers.Where(x => x.Email.ToLower() == email.ToLower()).CosmosToListAsync();

            return filteredCustomers.Any();
        }

        public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().CosmosToListAsync();
        }

        public async Task<CustomerEntity> GetCustomerByIdAsync(string id)
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().Where(x => x.Id == id).CosmosFirstOrDefaultAsync();
        }

        public async Task<CustomerEntity> GetCustomerByEmailAsync(string email)
        {
            return await _client.ReadAllItemsAsIQueryable<CustomerEntity>().Where(x => x.Email.ToLower().Contains(email.ToLower()) || x.Email.ToLower() == email.ToLower()).CosmosFirstOrDefaultAsync();
        }

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