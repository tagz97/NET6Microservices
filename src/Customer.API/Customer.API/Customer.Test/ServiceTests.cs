namespace Customer.Test
{
    public class ServiceTests
    {
        private Mock<ICustomerService> _customerService = new();
        private Mock<ICustomerRepository> _customerRepository = new();

        [Fact]
        public async void CustomerService_GetCustomerById_ReturnsCustomer()
        {
            // Arrange
            CustomerResponse response = new() { Data = new(), ResponseCode = Framework.Enums.ResponseCode.No_Error };
            _customerService.Setup(x => x.GetCustomerByIdAsync(It.IsAny<string>())).ReturnsAsync(response);
            var customerService = _customerService.Object;

            // Act
            var result = await customerService.GetCustomerByIdAsync(It.IsAny<string>());

            // Assert
            Assert.True(result != null);
        }
    }
}