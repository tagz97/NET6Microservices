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
        public void CreateCustomerRequest_ValidRequestEmail_MustPass(string email)
        {
            // Arrange
            CreateCustomerRequest requestObject = new() { Email = email };
            CreateCustomerValidator validator = new();

            // Act
            var validationResult = validator.Validate(requestObject);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        [Theory]
        [InlineData("contoso@@")]
        [InlineData("contoso@gmail@")]
        [InlineData("contoso@")]
        [InlineData("contoso")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("c")]
        public void CreateCustomerRequest_InvalidRequestEmail_MustFail(string email)
        {
            // Arrange
            CreateCustomerRequest requestObject = new() { Email = email };
            CreateCustomerValidator validator = new();

            // Act
            var validationResult = validator.Validate(requestObject);

            // Assert
            Assert.False(validationResult.IsValid);
        }
    }
}
