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

        public void AddErrors(ModelBuilderResult otherResult)
        {
            // No need to do anything if there are no errors
            if (otherResult.IsSuccess)
                return;

            foreach (var error in otherResult.ErrorMessages)
                AddErrors(error.Key, error.Value);
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
        public ModelBuilderResult()
        {
        }

        public ModelBuilderResult(T model)
        {
            base.Model = model;
        }

        public void SetModel(T model) => base.Model = model;

        public new T? Model => base.Model as T;
    }
}
