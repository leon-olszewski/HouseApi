using HouseApi.DataTransfer;
using HouseApi.Models;

namespace HouseApi.Mappers
{
    public interface IListingMapper
    {
        ModelBuilderResult<Listing> MapIn(ListingDto dto);
        ListingDto MapOut(Listing model);
    }

    public class ListingMapper : IListingMapper
    {
        public ModelBuilderResult<Listing> MapIn(ListingDto dto)
        {
            var postalCodeResult = PostalCode.TryFromString(
                ValidationInputFactory.Create(nameof(ListingDto.PostalCode), dto.PostalCode));
            var provinceResult = Province.TryGetProvinceByNameCode(
                ValidationInputFactory.Create(nameof(ListingDto.Province), dto.Province));
            var addressResult = Address.TryConstruct(
                ValidationInputFactory.Create(nameof(ListingDto.Line1), dto.Line1),
                ValidationInputFactory.Create(nameof(ListingDto.Province), provinceResult.Model),
                ValidationInputFactory.Create(nameof(ListingDto.PostalCode), postalCodeResult.Model));
            var listingResult = Listing.TryConstruct(
                ValidationInputFactory.Create(nameof(ListingDto.Id), dto.Id),
                ValidationInputFactory.Create(nameof(ListingDto.Description), dto.Description),
                ValidationInputFactory.Create(Constants.OtherErrors, addressResult.Model));

            var ret = ModelBuilderResult<Listing>.WithCombinedErrors(postalCodeResult, provinceResult, addressResult, listingResult);

            if (ret.IsSuccess)
                ret.SetModel(listingResult.Model!);

            return ret;
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
