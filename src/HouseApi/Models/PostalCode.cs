using System;
using System.Text.RegularExpressions;

namespace HouseApi.Models
{
    public class PostalCode
    {
        private string _postalCode;

        private PostalCode(string postalCode)
        {
            _postalCode = postalCode;
        }

        public static PostalCode FromString(string postalCode)
        {
            // We want to format the input to look like "H0H0H0".
            // Conventionally people often write it with a space in the
            // middle: "H0H 0H0" which we want to support. Internally
            // though, we just store it without the space.

            // Remove spaces
            var postalCodeTransformed =
                string.Join(
                    string.Empty,
                    postalCode.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries))
                .ToUpperInvariant(); // Make sure it's all caps

            // Make sure it conforms to the postal code format
            var regex = new Regex(@"^[A-Z]\d[A-Z]\d[A-Z]\d$");
            if (!regex.IsMatch(postalCodeTransformed))
                throw new Exception();

            return new PostalCode(postalCodeTransformed);
        }

        public static implicit operator string(PostalCode postalCode) => postalCode._postalCode;
    }
}
