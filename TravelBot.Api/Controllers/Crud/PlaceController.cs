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
///     CRUD контроллер по работе с <see cref="PlaceApiModel" />
/// </summary>
[ApiController]
[Authorize]
[Route("api/places")]
public class PlaceController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IPlaceService placeService;
    private readonly IValidateService validateService;

    /// <summary>
    ///     ctor
    /// </summary>
    public PlaceController(IPlaceService placeService, IValidateService validateService, IMapper mapper)
    {
        this.placeService = placeService;
        this.validateService = validateService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получает место по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PlaceApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "PlaceGetById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await placeService.GetById(id, cancellationToken);

        return Ok(mapper.Map<PlaceApiModel>(result));
    }

    /// <summary>
    ///     Получает список всех мест
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<PlaceApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "PlaceGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await placeService.GetAll(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<PlaceApiModel>>(result));
    }

    /// <summary>
    ///     Добавляет новое место
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PlaceApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "PlaceCreate")]
    public async Task<IActionResult> Create(PlaceCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<PlaceCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await placeService.Create(createModel, cancellationToken);

        return Ok(mapper.Map<PlaceApiModel>(result));
    }

    /// <summary>
    ///     Редактирует место по идентификатору
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(PlaceApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "PlaceEdit")]
    public async Task<IActionResult> Edit(Guid id, PlaceCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<PlaceCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await placeService.Edit(id, createModel, cancellationToken);

        return Ok(mapper.Map<PlaceApiModel>(result));
    }

    /// <summary>
    ///     Удаляет место по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "PlaceDelete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await placeService.Delete(id, cancellationToken);

        return Ok();
    }
}