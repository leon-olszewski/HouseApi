using HouseApi.Database;
using HouseApi.DataTransfer;
using HouseApi.Mappers;
using HouseApi.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HouseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetListings(
            [FromServices] IListingRepository repo,
            [FromServices] IListingMapper mapper)
        {
            var listings = repo.GetListings()
                .Select(mapper.MapOut);

            return Ok(listings);
        }

        [HttpPost]
        public IActionResult CreateListing(
            [FromBody] ListingDto listingDto,
            [FromServices] IListingValidator validator,
            [FromServices] IListingMapper mapper,
            [FromServices] IListingRepository repo)
        {
            // Validate
            var validationResult = validator.Validate(listingDto);
            if (!validationResult.IsValid)
            {
                ModelState.AddModelErrors(validationResult.ErrorMessages);
                return ValidationProblem();
            }

            // Map
            var listingModel = mapper.MapIn(listingDto);

            // Save
            repo.GetListings().Add(listingModel);
            repo.Save();

            return Ok();
        }
    }
}
