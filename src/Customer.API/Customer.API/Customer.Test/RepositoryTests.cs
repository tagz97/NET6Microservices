namespace Customer.Test
{
    public class RepositoryTests
    {
        private Mock<ICustomerRepository> _customerRepository = new();
        private Mock<ICosmosDbClient> _dbClient = new();

        [Fact]
        public async void CustomerRepository_GetCustomerById_ReturnsCustomer()
        {
            // Arrange
            CustomerEntity customer = new() { Email = "email@contoso.co.uk", Id = Guid.NewGuid().ToString() };
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);
            var customerRepository = _customerRepository.Object;

            // Act
            var result = await customerRepository.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result != null);
        }

        [Fact]
        public async void CustomerRepository_GetCustomerById_ReturnsNull()
        {
            // Arrange
            CustomerEntity customer = null;
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);
            var customerRepository = _customerRepository.Object;

            // Act
            var result = await customerRepository.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public async void CustomerRepository_CheckUniqueEmail_ReturnsFalse()
        {
            // Arrange
            _customerRepository.Setup(x => x.CheckCustomerEmailUniqueAsync(It.IsAny<string>())).ReturnsAsync(false);
            var customerRepository = _customerRepository.Object;

            // Act
            var result = await customerRepository.CheckCustomerEmailUniqueAsync("contoso@aol.com");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void CustomerRepository_CheckUniqueEmail_ReturnsTrue()
        {
            // Arrange
            _customerRepository.Setup(x => x.CheckCustomerEmailUniqueAsync(It.IsAny<string>())).ReturnsAsync(true);
            var customerRepository = _customerRepository.Object;

            // Act
            var result = await customerRepository.CheckCustomerEmailUniqueAsync("contoso@aol.com");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async void CustomerRepository_GetAllCustomersAsync_ReturnsCustomers()
        {
            List<CustomerEntity> customerEntities = new() { new CustomerEntity() };
            _customerRepository.Setup(x => x.GetAllCustomersAsync()).ReturnsAsync(customerEntities);
            var repository = _customerRepository.Object;

            var result = await repository.GetAllCustomersAsync();

            Assert.True(result.Any());
        }

        [Fact]
        public async void CustomerRepository_GetAllCustomersAsync_ReturnsNoCustomers()
        {
            List<CustomerEntity> customerEntities = new();
            _customerRepository.Setup(x => x.GetAllCustomersAsync()).ReturnsAsync(customerEntities);
            var repository = _customerRepository.Object;

            var result = await repository.GetAllCustomersAsync();

            Assert.False(result.Any());
        }
    }
}
