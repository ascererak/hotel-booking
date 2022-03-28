using System.Threading.Tasks;
using HotelBooking.Contracts.DTO;
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

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get() => Json(await hotelService.Get());

        [Route("search")]
        [HttpPut]
        public async Task<IActionResult> Search(SearchData searchData)
        {
            return Json(await hotelService.Get(searchData));
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id) => Json(await hotelService.Get(id));
    }
}