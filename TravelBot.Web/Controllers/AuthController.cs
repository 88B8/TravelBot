using Microsoft.AspNetCore.Mvc;
using TravelBot.Services.Contracts.Auth;
using TravelBot.Shared.Models;

namespace TravelBot.Web.Controllers
{
    /// <summary>
    /// Контроллер аутентификации
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        /// <summary>
        /// ctor
        /// </summary>
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestModel request, CancellationToken cancellationToken)
        {
            var token = await authService.Login(request, cancellationToken);

            if (token == null)
            {
                return Unauthorized();
            }

            HttpContext.Response.Cookies.Append("cookies", token);

            return Ok(token);
        }
    }
}