using FluentValidation.Results;

namespace Validation
{
    /// <summary>
    /// Validation result from deserializing a request and validating it
    /// </summary>
    /// <typeparam name="T">Validation result object type</typeparam>
    public class ValidationResult<T>
    {
        /// <summary>
        /// Value that has been deserialized and validated
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Determines if the request object is valid
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// Errors gathered during request validation
        /// </summary>
        public IList<ValidationFailure> Errors { get; set; }

        /// <summary>
        /// Generates a string representation of the error messages separated by new lines with a new error on each line
        /// </summary>
        /// <returns>Error messages separated by new lines. If string is null returs empty string</returns>
        public override string ToString()
            => Errors is null ? string.Empty : string.Join("\n", Errors.Select(failure => failure?.ErrorMessage));
    }
}