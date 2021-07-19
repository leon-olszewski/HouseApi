using HouseApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HouseApi
{
    public static class Extensions
    {
        public static void AddModelErrors(this ModelStateDictionary @this, ModelBuilderResult result)
        {
            foreach (var errorKvp in result.ErrorMessages)
            {
                foreach (var error in errorKvp.Value)
                {
                    @this.AddModelError(errorKvp.Key, error);
                }
            }
        }
    }
}
