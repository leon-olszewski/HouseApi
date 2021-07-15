using System;

namespace HouseApi.Models
{
    public class Address
    {
        public Address(string line1, Province province, PostalCode postalCode)
        {
            if (line1.Length > 200)
                throw new Exception();

            Line1 = line1;
            Province = province;
            PostalCode = postalCode;
        }

        public string Line1 { get; }
        public Province Province { get; }
        public PostalCode PostalCode { get; }
    }
}
