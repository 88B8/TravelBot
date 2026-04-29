using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelBot.Api.Exceptions;
using TravelBot.Api.Models.CreateApiModels;
using TravelBot.Api.Models.ResponseApiModels;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Services.Crud;
using TravelBot.Services.Contracts.Services.Validation;

namespace TravelBot.Api.Controllers.Crud;

/// <summary>
///     CRUD контроллер по работе с <see cref="UserApiModel" />
/// </summary>
[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IUserService userService;
    private readonly IValidateService validateService;
    
    /// <summary>
    /// ctor
    /// </summary>
    public UserController(IUserService userService, IValidateService validateService, IMapper mapper)
    {
        this.userService = userService;
        this.validateService = validateService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получает пользователя по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "UserGetById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await userService.GetById(id, cancellationToken);

        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    ///     Получает пользователя по идентификатору телеграм
    /// </summary>
    [HttpGet("by-telegram/{telegramId:long}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "UserGetByTelegramId")]
    public async Task<IActionResult> GetByTelegramId(long telegramId, CancellationToken cancellationToken)
    {
        var result = await userService.GetByTelegramId(telegramId, cancellationToken);

        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    ///     Получает список всех пользователей
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<UserApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "UserGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await userService.GetAll(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<UserApiModel>>(result));
    }

    /// <summary>
    ///     Добавляет нового пользователя
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "UserCreate")]
    public async Task<IActionResult> Create(UserCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<UserCreateModel>(request);

        await validateService.Validate(createModel, cancellationToken);

        var result = await userService.Create(createModel, cancellationToken);

        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    ///     Редактирует пользователя по идентификатору
    /// </summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "User,Admin")]
    [ProducesResponseType(typeof(UserApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "UserEdit")]
    public async Task<IActionResult> Edit(Guid id, UserCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<UserCreateModel>(request);

        await validateService.Validate(createModel, cancellationToken);

        var result = await userService.Edit(id, createModel, cancellationToken);

        return Ok(mapper.Map<UserApiModel>(result));
    }

    /// <summary>
    ///     Удаляет пользователя по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "UserDelete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await userService.Delete(id, cancellationToken);

        return Ok();
    }
}