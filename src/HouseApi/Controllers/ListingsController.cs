using HouseApi.Database;
using HouseApi.DataTransfer;
using HouseApi.Mappers;
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
            [FromServices] IListingMapper mapper,
            [FromServices] IListingRepository repo)
        {
            // Map + validate
            var listingModelResult = mapper.MapIn(listingDto);
            if (!listingModelResult.IsSuccess)
            {
                ModelState.AddModelErrors(listingModelResult);
                return ValidationProblem();
            }

            // Save
            repo.GetListings().Add(listingModelResult.Model!);
            repo.Save();

            return Ok();
        }
    }
}
