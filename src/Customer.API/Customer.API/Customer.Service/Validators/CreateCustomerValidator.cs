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
            RuleFor(customer => customer.Email).Cascade(CascadeMode.Stop)
                .MaximumLength(64).WithMessage("Maximum length for email exceeded")
                .MinimumLength(1).WithMessage("Email is required and cannot be empty")
                .Must(IsValidEmail).WithMessage("Email is not in correct format");
        }

        /// <summary>
        /// Check if the email is valid format
        /// </summary>
        /// <param name="email">Email to check</param>
        /// <returns>Whether email is valid or not</returns>
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
