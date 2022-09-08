using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Validation
{
    public class ValidationResult<T>
    {
        public T Value { get; set; }
        public bool IsValid { get; set; }
        public IList<ValidationFailure> Errors { get; set; }

        /// <summary>
        /// Generates a string representation of the error messages separated by new lines with a new error on each line
        /// </summary>
        /// <returns>Error messages separated by new lines. If <c>Errors</c> is null returs empty string</returns>
        public override string ToString()
            => Errors is null ? string.Empty : string.Join("\n", Errors.Select(failure => failure?.ErrorMessage));
    }
}