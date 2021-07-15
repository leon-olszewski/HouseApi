using HouseApi.DataTransfer;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace HouseApi.Validators
{
    public interface IListingValidator
    {
        ValidationResult Validate(ListingDto listing);
    }

    public class ListingValidator : IListingValidator
    {
        public ValidationResult Validate(ListingDto listing)
        {
            var ret = new ValidationResult();

            if (listing.Description == null)
            {
                ret.AddError(
                    key: nameof(ListingDto.Description),
                    error: "Value is required.");

                return ret;
            }
            else
            {
                const int descriptionMaxLength = 3000;
                if (listing.Description.Length > descriptionMaxLength)
                {
                    ret.AddError(
                        key: nameof(ListingDto.Description),
                        error: $"Length must not exceed {descriptionMaxLength} characters.");
                }
            }

            if (listing.Line1 == null)
            {
                ret.AddError(
                    key: nameof(ListingDto.Line1),
                    error: "Value is required.");

                return ret;
            }
            else
            {
                const int line1MaxLength = 200;
                if (listing.Line1.Length > line1MaxLength)
                {
                    ret.AddError(
                        key: nameof(ListingDto.Line1),
                        error: $"Length must not exceed {line1MaxLength} characters.");
                }
            }

            if (listing.Province == null)
            {
                ret.AddError(
                    key: nameof(ListingDto.Province),
                    error: "Value is required.");

                return ret;
            }
            else
            {
                var provinces = new string[]
                {
                    "BC", "AB", "SK", "MB", "ON", "QC", "NL", "PE", "NS", "NB", "YT", "NT", "NU"
                };
                if (provinces.All(p => p != listing.Province))
                {
                    ret.AddError(
                        key: nameof(ListingDto.Province),
                        error: $"Value must be a valid province code.");
                }
            }

            if (listing.PostalCode == null)
            {
                ret.AddError(
                    key: nameof(ListingDto.PostalCode),
                    error: "Value is required.");

                return ret;
            }
            else
            {
                var regex = new Regex(@"^[A-Z]\d[A-Z]\d[A-Z]\d$");
                var postalCodeTransformed =
                string.Join(
                    string.Empty,
                    listing.PostalCode.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries))
                .ToUpperInvariant(); // Make sure it's all caps

                // Make sure it conforms to the postal code format
                if (!regex.IsMatch(postalCodeTransformed))
                {
                    ret.AddError(
                        key: nameof(ListingDto.PostalCode),
                        error: "Value is not a valid postal code.");
                }
            }

            return ret;
        }
    }
}
