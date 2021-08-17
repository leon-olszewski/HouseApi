using System;
using System.Collections.Generic;

namespace HouseApi.Models
{
    public class Listing
    {
        private Listing(Guid id, string description, Address address)
        {
            Id = id;
            Description = description;
            Address = address;
        }

        public Guid Id { get; }
        public string Description { get; }
        public Address Address { get; }

        public static Listing Construct(Guid id, string description, Address address)
        {
            // Implicitly convert these to validation inputs
            var result = TryConstruct(id, description, address);
            result.ThrowIfNotSuccess();

            return result.Model!;
        }

        public static ModelBuilderResult<Listing> TryConstruct(
            ValidationInput<Guid?> id,
            ValidationInput<string?> description,
            ModelValidationInput<Address> address)
        {
            var result = new ModelBuilderResult<Listing>();

            if (id.Value == null)
                result.AddRequiredValidationError(id.Key);

            // Try to get the address from input. Add errors if it fails.
            var addressInputResult = address.TryGetModel();
            result.AddErrors(addressInputResult);

            if (description.Value == null)
                result.AddRequiredValidationError(description.Key);
            else
            {
                const int maxLength = 3000;
                if (description!.Value.Length > maxLength)
                {
                    result.AddMaxLengthValidationError(description.Key, maxLength);
                    return result;
                }
            }
 
            if (!result.IsSuccess)
                return result;

            result.SetModel(new Listing(id!.Value!.Value, description.Value!, addressInputResult.Model!));
            return result;
        }

        /// <summary>
        /// Implements basic entity equality semantics. Two entities are considered
        /// "equal" when their IDs match.
        /// </summary>
        public class ListingEntityEqualityComparer : IEqualityComparer<Listing>
        {
            public bool Equals(Listing? x, Listing? y)
            {
                if (x == null && y == null)
                    return true;

                if (x != null && y != null)
                    return x.Id == y.Id;

                return false;
            }

            public int GetHashCode(Listing obj) => obj?.Id.GetHashCode() ?? 0;
        }
    }
}
