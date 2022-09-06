namespace Customer.Test
{
    public class ServiceCollectionTests
    {
        private ServiceCollection services;

        [Fact]
        public void AddCustomerService_ServiceInjected()
        {
            // Arrange
            services = new();
            
            // Act
            services.AddCustomerService();
            var result = services.Any(x => x.ServiceType == typeof(ICustomerService));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AddCustomerRepository_ServiceInjected()
        {
            // Arrange
            services = new();

            // Act
            services.AddCustomerRepository();
            var result = services.Any(x => x.ServiceType == typeof(ICustomerRepository));

            // Assert
            Assert.True(result);
        }
    }
}
