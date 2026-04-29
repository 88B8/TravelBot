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
///     CRUD контроллер по работе с <see cref="PassportApiModel" />
/// </summary>
[ApiController]
[Authorize]
[Route("api/passports")]
public class PassportController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IPassportService passportService;
    private readonly IValidateService validateService;

    /// <summary>
    ///     ctor
    /// </summary>
    public PassportController(IPassportService passportService, IValidateService validateService, IMapper mapper)
    {
        this.passportService = passportService;
        this.validateService = validateService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получает паспорт по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "PassportGetById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await passportService.GetById(id, cancellationToken);

        return Ok(mapper.Map<PassportApiModel>(result));
    }

    /// <summary>
    ///     Получает список всех паспортов
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<PassportApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "PassportGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await passportService.GetAll(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<PassportApiModel>>(result));
    }

    /// <summary>
    ///     Добавляет новый паспорт
    /// </summary>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "PassportCreate")]
    public async Task<IActionResult> Create(PassportCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<PassportCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await passportService.Create(createModel, cancellationToken);

        return Ok(mapper.Map<PassportApiModel>(result));
    }

    /// <summary>
    ///     Редактирует паспорт по идентификатору
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "PassportEdit")]
    public async Task<IActionResult> Edit(Guid id, PassportCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<PassportCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await passportService.Edit(id, createModel, cancellationToken);

        return Ok(mapper.Map<PassportApiModel>(result));
    }

    /// <summary>
    ///     Удаляет паспорт по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "PassportDelete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await passportService.Delete(id, cancellationToken);

        return Ok();
    }
}