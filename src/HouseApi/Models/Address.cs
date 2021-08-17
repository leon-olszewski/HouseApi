namespace HouseApi.Models
{
    public class Address
    {
        private Address(string line1, Province province, PostalCode postalCode)
        {
            Line1 = line1;
            Province = province;
            PostalCode = postalCode;
        }

        public string Line1 { get; }
        public Province Province { get; }
        public PostalCode PostalCode { get; }

        public static Address Construct(string line1, Province province, PostalCode postalCode)
        {
            // Use implicit conversion to convert these to validation inputs
            var result = TryConstruct(line1, province, postalCode);
            result.ThrowIfNotSuccess();

            return result.Model!;
        }

        public static ModelBuilderResult<Address> TryConstruct(
            ValidationInput<string?> line1,
            ModelValidationInput<Province> province,
            ModelValidationInput<PostalCode> postalCode)
        {
            var result = new ModelBuilderResult<Address>();

            if (line1.Value == null)
                result.AddRequiredValidationError(line1.Key);
            else
            {
                const int maxLength = 200;
                if (line1!.Value.Length > maxLength)
                {
                    result.AddMaxLengthValidationError(line1.Key, maxLength);
                    return result;
                }
            }

            // Try to get the province from input. Add errors if it fails.
            var provinceInputResult = province.TryGetModel();
            result.AddErrors(provinceInputResult);

            // Try to get the postal code from input. Add errors if it fails.
            var postalCodeInputResult = postalCode.TryGetModel();
            result.AddErrors(postalCodeInputResult);

            if (!result.IsSuccess)
                return result;

            result.SetModel(new Address(line1.Value!, provinceInputResult.Model!, postalCodeInputResult.Model!));
            return result;
        }
    }
}
