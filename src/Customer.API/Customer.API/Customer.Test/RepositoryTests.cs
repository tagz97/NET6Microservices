namespace Customer.Test
{
    public class RepositoryTests
    {
        private Mock<ICustomerRepository> _customerRepository = new();

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
    }
}
