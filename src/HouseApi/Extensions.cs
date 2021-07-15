using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace HouseApi
{
    public static class Extensions
    {
        public static void AddModelErrors(this ModelStateDictionary @this, IDictionary<string, string> errors)
        {
            foreach (var errorKeyValuePair in errors)
            {
                @this.AddModelError(errorKeyValuePair.Key, errorKeyValuePair.Value);
            }

        }
    }
}
