using System;
using System.Collections.Generic;

namespace HouseApi.Models
{
    public class Listing
    {
        public Listing(Guid id, string description, Address address)
        {
            if (description.Length > 3000)
                throw new Exception();

            Id = id;
            Description = description;
            Address = address;
        }

        public Guid Id { get; }
        public string Description { get; }
        public Address Address { get; }

        public class EntityEqualityComparer : IEqualityComparer<Listing>
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
