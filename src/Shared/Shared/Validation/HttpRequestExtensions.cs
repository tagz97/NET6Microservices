using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Validation
{
    /// <summary>
    /// Http request extensions for getting the body of an http request and applying validation
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Get the body of the http request and validate it
        /// </summary>
        /// <typeparam name="T">Type of entity to deserialize to</typeparam>
        /// <typeparam name="V">Validator to be applied</typeparam>
        /// <param name="request">The http request</param>
        /// <returns>Validation result <see cref="ValidationResult{T}"/></returns>
        public static async Task<ValidationResult<T>> GetBody<T, V>(this HttpRequest request)
            where V : Validator<T>, new()
        {
            return await request.GetBody<T, V>(null);
        }

        /// <summary>
        /// Get the body of the http request and validate it
        /// </summary>
        /// <typeparam name="T">Type of entity to deserialize to</typeparam>
        /// <typeparam name="V">Validator to be applied</typeparam>
        /// <param name="request">The http request</param>
        /// <param name="parameter"></param>
        /// <returns>Validation result <see cref="ValidationResult{T}"/></returns>
        public static async Task<ValidationResult<T>> GetBody<T, V>(this HttpRequest request, object parameter)
            where V : Validator<T>, new()
        {
            var validator = new V
            {
                Parameter = parameter
            };

            try
            {
                var requestObject = await request.GetBody<T>();

                var validationResult = validator.Validate(requestObject);

                if (!validationResult.IsValid)
                {
                    return new ValidationResult<T>
                    {
                        Value = requestObject,
                        IsValid = false,
                        Errors = validationResult.Errors
                    };
                }

                return new ValidationResult<T>
                {
                    Value = requestObject,
                    IsValid = true
                };
            }
            catch (JsonException e)
            {
                using var streamReader = new StreamReader(request.Body);
                var requestBody = streamReader.ReadToEnd();

                string errorMessage =
                    $"Request JSON is malformed:\n" +
                    $"{e.Message}" +
                    $"\nRequest JSON:\n" +
                    $"{requestBody}";

                var validationFailure = new ValidationFailure("JSON", errorMessage);

                return new ValidationResult<T>
                {
                    IsValid = false,
                    Errors = new[] { validationFailure }
                };
            }
        }

        /// <summary>
        /// Get the body of the http request as a deserialized object
        /// </summary>
        /// <typeparam name="T">Object to deserialize to</typeparam>
        /// <param name="request">The http request</param>
        /// <returns>Deseralized object with type T</returns>
        public static async Task<T> GetBody<T>(this HttpRequest request)
        {
            var requestBody = await request.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<T>(requestBody, options);
        }
    }
}
