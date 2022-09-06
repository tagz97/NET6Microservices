namespace Customer.Test
{
    public class ServiceCollectionTests
    {
        private ServiceCollection services;

        [Fact]
        public void AddCustomerService_ServiceInjected()
        {
            services = new();
            
            services.AddCustomerService();

            var result = services.Any(x => x.ServiceType == typeof(ICustomerService));

            Assert.True(result);
        }

        [Fact]
        public void AddCustomerRepository_ServiceInjected()
        {
            services = new();

            services.AddCustomerRepository();

            var result = services.Any(x => x.ServiceType == typeof(ICustomerRepository));

            Assert.True(result);
        }
    }
}
