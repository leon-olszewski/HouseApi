using HouseApi.DataTransfer;
using HouseApi.Models;

namespace HouseApi.Mappers
{
    public interface IListingMapper
    {
        Listing MapIn(ListingDto dto);
        ListingDto MapOut(Listing model);
    }

    public class ListingMapper : IListingMapper
    {
        public Listing MapIn(ListingDto dto)
        {
            return new Listing
            (
                id: dto.Id!.Value,
                description: dto.Description!,
                address: new Address(
                    line1: dto.Line1!,
                    province: Province.GetProvinceByNameCode(dto.Province!),
                    postalCode: PostalCode.FromString(dto.PostalCode!)));
        }

        public ListingDto MapOut(Listing model)
        {
            return new ListingDto
            {
                Id = model.Id,
                Description = model.Description,
                Line1 = model.Address.Line1,
                Province = model.Address.Province,
                PostalCode = model.Address.PostalCode
            };
        }
    }
}
