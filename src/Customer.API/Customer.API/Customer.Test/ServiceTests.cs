namespace Customer.Test
{
    public class ServiceTests
    {
        private Mock<ICustomerRepository> _customerRepository = new();
        private Mock<ILogger<CustomerService>> _logger = new();
        private CustomerEntity _customer = new()
        {
            Email = "contoso@aol.com",
            Id = Guid.NewGuid().ToString(),
            PostCode = "GU22",
            FirstName = "Contoso",
            LastName = "Microsoft",
            MobileNumber = "07444444444",
            State = CustomerState.ACTIVE
        };

        private CustomerService CreateServiceFromMocks() => new(_customerRepository.Object, _logger.Object);

        [Fact]
        public async void CustomerService_DeleteCustomer_DeletionSuccess()
        {
            // Arrange
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(_customer.Id)).ReturnsAsync(_customer);
            _customerRepository.Setup(x => x.DeleteCustomerAsync(_customer)).ReturnsAsync(true);
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.DeleteCustomerAsync(_customer.Id);

            // Assert
            Assert.True(result.ResponseCode == ResponseCode.No_Error);
        }

        [Fact]
        public async void CustomerService_DeleteCustomer_DeletionFailure()
        {
            // Arrange
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(_customer.Id)).ReturnsAsync(_customer);
            _customerRepository.Setup(x => x.DeleteCustomerAsync(_customer)).ReturnsAsync(false);
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.DeleteCustomerAsync(_customer.Id);

            // Assert
            Assert.True(result.ResponseCode == ResponseCode.Delete_Fail);
        }

        [Fact]
        public async void CustomerService_DeleteCustomer_CustomerNotFound()
        {
            // Arrange
            _customer = new();
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(_customer.Id)).ReturnsAsync(_customer);
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.DeleteCustomerAsync(_customer.Id);

            // Assert
            Assert.True(result.ResponseCode == ResponseCode.Delete_Fail);
        }

        [Fact]
        public async void CustomerService_DeleteCustomer_ThrowsException()
        {
            // Arrange
            _customer = new();
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(_customer.Id)).ThrowsAsync(new Exception());
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.DeleteCustomerAsync(_customer.Id);

            // Assert
            Assert.True(result.ResponseCode == ResponseCode.Delete_Fail);
        }

        [Fact]
        public async void CustomerService_GetCustomerById_ReturnsCustomer()
        {
            // Arrange
            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(_customer);
            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result != null);
            Assert.Equal(result.Data.FirstName, _customer.FirstName);
            Assert.Equal(result.Data.LastName, _customer.LastName);
            Assert.Equal(result.Data.MobileNumber, _customer.MobileNumber);
            Assert.Equal(result.Data.PostCode, _customer.PostCode);
            Assert.Equal(result.Data.State, _customer.State);
            Assert.Equal(result.Data.Id, _customer.Id);
        }

        [Fact]
        public async void CustomerService_GetCustomerById_ReturnsNull()
        {
            // Arrange
            _customer = null;

            _customerRepository.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(_customer);

            var customerService = CreateServiceFromMocks();

            // Act
            var result = await customerService.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result.Data == null);
        }

        [Theory]
        [MemberData(nameof(ListOfCreateCustomerRequest))]
        public async void CustomerService_CreateCustomerFromRequestAsync_ReturnsCustomer(CreateCustomerRequest createCustomerRequest)
        {
            // Arrange
            _customer = new(createCustomerRequest);

            var res = JsonSerializer.Serialize(createCustomerRequest);
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(res));
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Body = memoryStream;

            _customerRepository.Setup(x => x.AddCustomerAsync(_customer)).ReturnsAsync(_customer);
            _customerRepository.Setup(x => x.CheckCustomerEmailUniqueAsync(_customer.Email)).ReturnsAsync(true);

            var service = CreateServiceFromMocks();

            // Act
            var result = await service.CreateCustomerFromRequestAsync(httpContext.Request);

            // Assert
            Assert.NotNull(result);
        }

        public static IEnumerable<object[]> ListOfCreateCustomerRequest =>
                new List<object[]>
                {
                    new object[] { new CreateCustomerRequest() { Email = "test1@aol.co.uk" } },
                    new object[] { new CreateCustomerRequest() { Email = "test2@aol.com" } },
                    new object[] { new CreateCustomerRequest() { Email = "test3@aol.co.za" } },
                    new object[] { new CreateCustomerRequest() { Email = "test4@gov.uk" } },
                };
    }
}