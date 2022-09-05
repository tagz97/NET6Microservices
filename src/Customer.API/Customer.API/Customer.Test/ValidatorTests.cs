namespace Customer.Test
{
    public class ValidatorTests
    {
        [Theory]
        [InlineData("contoso@gmail.com")]
        [InlineData("contoso@gmail.co.uk")]
        [InlineData("contoso@gov.co.uk")]
        [InlineData("contoso@aol.com")]
        [InlineData("contoso@aol.co.za")]
        public void CreateCustomerRequest_ValidRequest_MustPass(string email)
        {
            // Arrange
            CreateCustomerRequest requestObject = new() { Email = email };
            CreateCustomerValidator validator = new();

            // Act
            var validationResult = validator.Validate(requestObject);

            // Assert
            Assert.True(validationResult.IsValid);
        }
    }
}
