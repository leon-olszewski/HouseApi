using HouseApi.DataTransfer;
using HouseApi.Models;
using System;

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
            Func<ModelBuilderResult<Province>> provinceGetter = () =>
               Province.TryGetProvinceByNameCode(
                   new ValidationInput<string?>(nameof(ListingDto.Province), dto.Province));

            Func<ModelBuilderResult<PostalCode>> postalCodeGetter = () =>
                PostalCode.TryFromString(
                    new ValidationInput<string?>(nameof(ListingDto.PostalCode), dto.PostalCode));

            Func<ModelBuilderResult<Address>> addressGetter = () =>
                Address.TryConstruct(
                    new ValidationInput<string?>(nameof(ListingDto.Line1), dto.Line1),
                    new ModelValidationInput<Province>(provinceGetter),
                    new ModelValidationInput<PostalCode>(postalCodeGetter));

            return Listing.TryConstruct(
                new ValidationInput<Guid?>(nameof(ListingDto.Id), dto.Id),
                new ValidationInput<string?>(nameof(ListingDto.Description), dto.Description),
                new ModelValidationInput<Address>(addressGetter));
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
