namespace ServiceCollectionExtensions.Test
{
    public class ServiceCollectionExtensionTests
    {
        private ServiceCollection _services;

        [Fact]
        public void ServiceExtensions_AddSingleton_Success()
        {
            // Arrange
            _services = new();

            // Act
            _services.AddService<IServiceCollection, ServiceCollection>(ServiceType.SINGLETON);
            var result = _services.Any(x => x.ServiceType == typeof(IServiceCollection));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ServiceExtensions_AddScoped_Success()
        {
            // Arrange
            _services = new();

            // Act
            _services.AddService<IServiceCollection, ServiceCollection>(ServiceType.SCOPED);
            var result = _services.Any(x => x.ServiceType == typeof(IServiceCollection));

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ServiceExtensions_AddTransient_Success()
        {
            // Arrange
            _services = new();

            // Act
            _services.AddService<IServiceCollection, ServiceCollection>(ServiceType.TRANSIENT);
            var result = _services.Any(x => x.ServiceType == typeof(IServiceCollection));

            // Assert
            Assert.True(result);
        }
    }
}