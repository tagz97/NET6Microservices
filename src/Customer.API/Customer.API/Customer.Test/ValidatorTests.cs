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

        [Theory]
        [MemberData(nameof(ListOfInvalidEmails))]
        public void CustomerEmailValidator_InvalidEmail_MustFail(string email)
        {
            // Arrange
            CustomerEmailValidator validator = new();

            // Act
            var validationResult = validator.Validate(email);

            // Assert
            Assert.False(validationResult.IsValid);
        }

        [Theory]
        [MemberData(nameof(ListOfValidEmails))]
        public void CustomerEmailValidator_ValidEmail_MustPass(string email)
        {
            // Arrange
            CustomerEmailValidator validator = new();

            // Act
            var validationResult = validator.Validate(email);

            // Assert
            Assert.True(validationResult.IsValid);
        }

        /// <summary>
        /// A list of emails with invalid format
        /// </summary>
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

        /// <summary>
        /// A list of emails with valid format
        /// </summary>
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
