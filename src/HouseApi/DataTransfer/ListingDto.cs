using System;

namespace HouseApi.DataTransfer
{
    public class ListingDto
    {
        public Guid? Id { get; set; }
        public string? Description { get; set; }
        public string? Line1 { get; set; }
        public string? Province { get; set; }
        public string? PostalCode { get; set; }
    }
}
