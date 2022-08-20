using Microsoft.AspNetCore.Mvc;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Application.Requests.Account;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IAccountLogic _logic;

        public AccountController(IAccountLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _logic.LoginAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(await _logic.RegisterAsync(request));
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            return Ok(await _logic.RefreshTokenAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile(string userName)
        {
            return Ok(await _logic.GetProfileAsync(userName));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        {
            return Ok(await _logic.UpdateProfileAsync(request));
        }
    }
}
