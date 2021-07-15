using System.Collections.Generic;
using System.Linq;

namespace HouseApi.Validators
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ErrorMessages = new Dictionary<string, string>();
        }

        public void AddError(string key, string error)
        {
            ErrorMessages.Add(key, error);
        }

        public bool IsValid => !ErrorMessages.Any();
        public IDictionary<string, string> ErrorMessages { get; private set; }
    }
}
