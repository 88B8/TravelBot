using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelBot.Api.Models.RequestModels;
using TravelBot.Services.Contracts.Auth;
using TravelBot.Services.Contracts.Models.Auth;

namespace TravelBot.Api.Controllers.Auth;

/// <summary>
///     Контроллер аутентификации
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    /// <summary>
    /// ctor
    /// </summary>
    public AuthController(IAuthService authService, IMapper mapper)
    {
        this.authService = authService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Авторизация администратора
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerOperation(OperationId = "AuthLogin")]
    public async Task<IActionResult> Login(LoginRequestApiModel model, CancellationToken cancellationToken)
    {
        var request = mapper.Map<LoginRequestModel>(model);

        var token = await authService.Login(request, cancellationToken);

        if (token is null)
            return Unauthorized();

        HttpContext.Response.Cookies.Append(
            "cookies",
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

        return Ok(token);
    }
}