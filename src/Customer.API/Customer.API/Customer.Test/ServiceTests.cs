namespace Customer.Test
{
    public class ServiceTests
    {
        private Mock<ICustomerService> _customerService = new();
        private Mock<ICustomerRepository> _customerRepository = new();
        private Mock<ILogger<CustomerService>> _logger = new();

        private CustomerService CreateServiceFromMocks() => new(_customerRepository.Object, _logger.Object);

        [Fact]
        public async void CustomerService_GetCustomerById_ReturnsCustomer()
        {
            // Arrange
            CustomerEntity customer = new()
            {
                Email = "contoso@aol.com",
                Id = Guid.NewGuid().ToString()
            };

            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result != null);
        }

        [Fact]
        public async void CustomerService_GetCustomerById_ReturnsNull()
        {
            // Arrange
            CustomerEntity customer = null;

            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(customer);

            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result.Data == null);
        }

        [Fact]
        public async void CustomerService_CreateCustomerFromRequestAsync_ReturnsCustomer()
        {
            // Arrange
            CustomerEntity customer = new()
            {
                Email = "contoso@aol.com",
                Id = Guid.NewGuid().ToString()
            };

            CreateCustomerRequest createCustomerRequest = new()
            {
                Email = "contoso@aol.co.uk"
            };

            var res = JsonSerializer.Serialize(createCustomerRequest);
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(res));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Body = memoryStream;

            _customerRepository.Setup(x => x.AddCustomerAsync(customer)).ReturnsAsync(customer);
            _customerRepository.Setup(x => x.CheckCustomerEmailUniqueAsync(customer.Email)).ReturnsAsync(true);

            var service = CreateServiceFromMocks();

            // Act
            var result = await service.CreateCustomerFromRequestAsync(httpContext.Request);

            // Assert
            Assert.NotNull(result);
        }
    }
}