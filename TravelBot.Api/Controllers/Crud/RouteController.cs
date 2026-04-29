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
///     CRUD контроллер по работе с <see cref="RouteApiModel" />
/// </summary>
[ApiController]
[Authorize]
[Route("api/routes")]
public class RouteController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly IRouteService routeService;
    private readonly IValidateService validateService;

    /// <summary>
    ///     ctor
    /// </summary>
    public RouteController(IRouteService routeService, IValidateService validateService, IMapper mapper)
    {
        this.routeService = routeService;
        this.validateService = validateService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получает маршрут по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "RouteGetById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await routeService.GetById(id, cancellationToken);

        return Ok(mapper.Map<RouteApiModel>(result));
    }

    /// <summary>
    ///     Получает список всех маршрутов
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IReadOnlyCollection<RouteApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "RouteGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await routeService.GetAll(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<RouteApiModel>>(result));
    }

    /// <summary>
    ///     Получает список всех активных маршрутов
    /// </summary>
    [HttpGet("active")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IReadOnlyCollection<RouteApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "RouteGetAllActive")]
    public async Task<IActionResult> GetAllActive(CancellationToken cancellationToken)
    {
        var result = await routeService.GetAllActive(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<RouteApiModel>>(result));
    }

    /// <summary>
    ///     Добавляет новый маршрут
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "RouteCreate")]
    public async Task<IActionResult> Create(RouteCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<RouteCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await routeService.Create(createModel, cancellationToken);

        return Ok(mapper.Map<RouteApiModel>(result));
    }

    /// <summary>
    ///     Редактирует маршрут по идентификатору
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "RouteEdit")]
    public async Task<IActionResult> Edit(Guid id, RouteCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<RouteCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await routeService.Edit(id, createModel, cancellationToken);

        return Ok(mapper.Map<RouteApiModel>(result));
    }

    /// <summary>
    ///     Удаляет маршрут по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "RouteDelete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await routeService.Delete(id, cancellationToken);

        return Ok();
    }
}