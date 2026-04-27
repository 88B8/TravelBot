using AutoMapper;
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
    ///     ctor
    /// </summary>
    public AuthController(IAuthService authService, IMapper mapper)
    {
        this.authService = authService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Аутентификация
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "AuthLogin")]
    public async Task<IActionResult> Login(LoginRequestApiModel model, CancellationToken cancellationToken)
    {
        var request = mapper.Map<LoginRequestModel>(model);
        var token = await authService.Login(request, cancellationToken);

        if (token == null) return Unauthorized();

        HttpContext.Response.Cookies.Append("cookies", token);

        return Ok(token);
    }
}