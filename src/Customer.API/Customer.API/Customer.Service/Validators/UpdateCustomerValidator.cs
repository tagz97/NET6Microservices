using Customer.Domain.Models.Requests;
using Customer.Service.Validators.CustomerEntityPropertyValidators;
using Validation;

namespace Customer.Service.Validators
{
    public sealed class UpdateCustomerValidator : Validator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(customer => customer.Email)
                .SetValidator(new CustomerEmailValidator());
        }
    }
}
