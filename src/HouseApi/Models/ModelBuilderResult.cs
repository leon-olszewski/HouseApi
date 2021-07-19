using System.Collections.Generic;
using System.Linq;

namespace HouseApi.Models
{
    public class ModelBuilderResult
    {
        public ModelBuilderResult()
        {
            ErrorMessages = new Dictionary<string, List<string>>();
        }

        public void AddError(string key, string error)
        {
            if (!ErrorMessages.TryGetValue(key, out var errorsForKey))
                errorsForKey = new List<string>();

            errorsForKey.Add(error);
            ErrorMessages[key] = errorsForKey;
        }

        public void AddErrors(string key, IEnumerable<string> errors)
        {
            if (!ErrorMessages.TryGetValue(key, out var errorsForKey))
                errorsForKey = new List<string>();

            errorsForKey.AddRange(errors);
            ErrorMessages[key] = errorsForKey;
        }

        public void SetModel(object model) => Model = model;

        public bool IsSuccess => !ErrorMessages.Any();
        public IDictionary<string, List<string>> ErrorMessages { get; }
        public object? Model { get; protected set; }
    }

    /// <summary>
    /// Carries either a model or error messages.
    /// </summary>
    public class ModelBuilderResult<T> : ModelBuilderResult where T : class
    {
        public void SetModel(T model) => base.Model = model;

        public new T? Model => base.Model as T;

        /// <summary>
        /// You may want to combine results when they contain errors to make a
        /// new error result.
        /// </summary>
        public static ModelBuilderResult<T> WithCombinedErrors(params ModelBuilderResult[] otherResults)
        {
            var ret = new ModelBuilderResult<T>();

            foreach (var errorsDictionary in otherResults.Select(o => o.ErrorMessages))
            {
                foreach (var errorKvp in errorsDictionary)
                {
                    ret.AddErrors(errorKvp.Key, errorKvp.Value);
                }
            }

            return ret;
        }
    }
}
