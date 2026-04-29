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
///     CRUD-контроллер по работе с администраторами
/// </summary>
[ApiController]
[Authorize]
[Route("api/admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminService adminService;
    private readonly IMapper mapper;
    private readonly IValidateService validateService;

    /// <summary>
    ///     ctor
    /// </summary>
    public AdminController(IAdminService adminService, IValidateService validateService, IMapper mapper)
    {
        this.adminService = adminService;
        this.validateService = validateService;
        this.mapper = mapper;
    }

    /// <summary>
    ///     Получает администратора по идентификатору
    /// </summary>
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Owner")]
    [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "AdminGetById")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await adminService.GetById(id, cancellationToken);

        return Ok(mapper.Map<AdminApiModel>(result));
    }

    /// <summary>
    ///     Получает список всех администраторов
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Owner")]
    [ProducesResponseType(typeof(IReadOnlyCollection<AdminApiModel>), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "AdminGetAll")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await adminService.GetAll(cancellationToken);

        return Ok(mapper.Map<IReadOnlyCollection<AdminApiModel>>(result));
    }

    /// <summary>
    ///     Добавляет нового администратора
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Owner")]
    [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "AdminCreate")]
    public async Task<IActionResult> Create(AdminCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<AdminCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await adminService.Create(createModel, cancellationToken);

        return Ok(mapper.Map<AdminApiModel>(result));
    }

    /// <summary>
    ///     Редактирует администратора по идентификатору
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Owner")]
    [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
    [SwaggerOperation(OperationId = "AdminEdit")]
    public async Task<IActionResult> Edit(Guid id, AdminCreateApiModel request, CancellationToken cancellationToken)
    {
        var createModel = mapper.Map<AdminCreateModel>(request);
        await validateService.Validate(createModel, cancellationToken);
        var result = await adminService.Edit(id, createModel, cancellationToken);

        return Ok(mapper.Map<AdminApiModel>(result));
    }

    /// <summary>
    ///     Удаляет администратора по идентификатору
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Owner")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
    [SwaggerOperation(OperationId = "AdminDelete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await adminService.Delete(id, cancellationToken);

        return Ok();
    }
}
