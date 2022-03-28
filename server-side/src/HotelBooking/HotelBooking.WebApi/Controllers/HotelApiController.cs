using System.Threading.Tasks;
using HotelBooking.Contracts.Dto.Models.Search;
using HotelBooking.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.WebApi.Controllers
{
    [Route("api/hotel")]
    [ApiController]
    public class HotelApiController : Controller
    {
        private readonly IHotelService hotelService;

        public HotelApiController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get() => Json(await hotelService.GetAsync());

        [HttpPut]
        [Route("search-count")]
        public async Task<IActionResult> Count(DataForSearch searchData) => Json(await hotelService.CountSearchResultsAsync(searchData));

        [HttpPut]
        [Route("search")]
        public async Task<ActionResult> Search(DataForSearch searchData) => Json(await hotelService.GetSearchResultByPageAsync(searchData));

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(int id) => Json(await hotelService.GetAsync(id));
    }
}