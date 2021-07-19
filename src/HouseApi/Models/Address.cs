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
            ValidationInput<Province?> province,
            ValidationInput<PostalCode?> postalCode)
        {
            var result = new ModelBuilderResult<Address>();

            if (province.Value == null)
                result.AddRequiredValidationError(province.Key);

            if (postalCode.Value == null)
                result.AddRequiredValidationError(postalCode.Key);

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

            if (!result.IsSuccess)
                return result;

            result.SetModel(new Address(line1.Value!, province.Value!, postalCode.Value!));
            return result;
        }
    }
}
