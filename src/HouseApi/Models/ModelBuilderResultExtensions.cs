using System.Collections.Generic;
using System.Linq;
using System;

namespace HouseApi.Models
{
    /// <summary>
    /// Carries either a model or error messages.
    /// </summary>
    public static class ModelBuilderResultExtensions
    {
        public static void ThrowIfNotSuccess<T>(this ModelBuilderResult<T> result) where T : class
        {
            if (!result.IsSuccess)
                throw new Exception(GetErrorStringForException(result.ErrorMessages));
        }

        public static void AddRequiredValidationError<T>(this ModelBuilderResult<T> result, string key) where T : class
        {
            result.AddError(key, "Value is required.");
        }

        public static void AddMaxLengthValidationError<T>(this ModelBuilderResult<T> result, string key, int maxLength) where T : class
        {
            result.AddError(key, $"Max length of {maxLength} characters exceeded.");
        }

        private static string GetErrorStringForException(IDictionary<string, List<string>> errors)
        {
            var rows = errors.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}");
            return string.Join(Environment.NewLine, rows);
        }
    }
}
