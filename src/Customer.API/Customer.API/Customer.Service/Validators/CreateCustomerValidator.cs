using Customer.Domain.Models.Requests;
using Customer.Service.Validators.CustomerEntityPropertyValidators;
using Validation;

namespace Customer.Service.Validators
{
    /// <summary>
    /// Validation for Create Customer using Fluent Validation
    /// </summary>
    public sealed class CreateCustomerValidator : Validator<CreateCustomerRequest>
    {
        /// <summary>
        /// Setting the rules for the validation to take place
        /// </summary>
        public CreateCustomerValidator()
        {
            RuleFor(customer => customer.Email)
                .SetValidator(new CustomerEmailValidator());
        }
    }
}
