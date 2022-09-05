using FluentValidation;

namespace Validation
{
    public class Validator<T> : AbstractValidator<T>
    {
        public object Parameter { get; set; }
    }
}