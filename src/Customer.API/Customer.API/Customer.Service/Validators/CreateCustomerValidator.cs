using Customer.Domain.Models.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Validation;

namespace Customer.Service.Validators
{
    public sealed class CreateCustomerValidator : Validator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            RuleFor(customer => customer.Email).Cascade(CascadeMode.Stop)
                .MaximumLength(64).WithMessage("Maximum length for email exceeded")
                .MinimumLength(1).WithMessage("Email is required and cannot be empty")
                .Must(IsValidEmail).WithMessage("Email is not in correct format");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                MailAddress mail = new(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
