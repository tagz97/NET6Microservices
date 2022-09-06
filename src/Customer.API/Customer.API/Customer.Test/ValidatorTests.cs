namespace Customer.Test
{
    public class ValidatorTests
    {
        [Theory]
        [MemberData(nameof(ListOfValidEmails))]
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
        [MemberData(nameof(ListOfInvalidEmails))]
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

        public static IEnumerable<object[]> ListOfInvalidEmails =>
                new List<object[]>
                {
                    new object[] { "contoso@@" },
                    new object[] { "contoso@gmail@" } ,
                    new object[] { "contoso@" },
                    new object[] { "contoso" },
                    new object[] { "" },
                    new object[] { " " },
                    new object[] { "c" },
                };

        public static IEnumerable<object[]> ListOfValidEmails =>
                new List<object[]>
                {
                    new object[] { "test1@aol.co.uk" },
                    new object[] { "test2@outlook.com" } ,
                    new object[] { "test3@gmail.co.za" },
                    new object[] { "test4@gov.uk" },
                };
    }
}
