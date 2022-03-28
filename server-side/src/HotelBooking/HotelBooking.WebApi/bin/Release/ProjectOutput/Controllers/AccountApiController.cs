using System.Threading.Tasks;
using HotelBooking.Contracts.DTO;
using HotelBooking.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Services.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountApiController : Controller
    {
        private readonly IAccountService accountService;

        public AccountApiController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserData userData)
            => Json(await accountService.Register(userData));

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(UserLogInData userLogInData)
            => Json(await accountService.Login(userLogInData));

        [Route("logout")]
        [HttpPost]
        public IActionResult Logout(SessionData sessionData)
        {
            accountService.Logout(sessionData);
            return Ok();
        }
    }
}