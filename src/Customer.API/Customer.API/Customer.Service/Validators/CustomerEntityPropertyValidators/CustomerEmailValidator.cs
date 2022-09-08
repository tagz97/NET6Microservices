using FluentValidation;
using System.Net.Mail;

namespace Customer.Service.Validators.CustomerEntityPropertyValidators
{
    /// <summary>
    /// Validator for Customer Email
    /// </summary>
    public sealed class CustomerEmailValidator : AbstractValidator<string>
    {
        public CustomerEmailValidator()
        {
            RuleFor(x => x).Cascade(CascadeMode.Stop)
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
