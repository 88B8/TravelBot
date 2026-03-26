using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Services;
using TravelBot.Web.Exceptions;
using TravelBot.Web.Models.CreateApiModels;
using TravelBot.Web.Models.ResponseApiModels;

namespace TravelBot.Web.Controllers
{
    /// <summary>
    /// CRUD-контроллер по работе с администраторами
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public AdminController(IAdminService adminService, IValidateService validateService, IMapper mapper)
        {
            this.adminService = adminService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает администратора по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await adminService.GetById(id, cancellationToken);

            return Ok(mapper.Map<AdminApiModel>(result));
        }

        /// <summary>
        /// Получает список всех администраторов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<AdminApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await adminService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<AdminApiModel>>(result));
        }

        /// <summary>
        /// Добавляет нового администратора
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(AdminCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<AdminCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await adminService.Create(createModel, cancellationToken);

            return Ok(mapper.Map<AdminApiModel>(result));
        }

        /// <summary>
        /// Редактирует администратора по идентификатору
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(AdminApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, AdminCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<AdminCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await adminService.Edit(id, createModel, cancellationToken);

            return Ok(mapper.Map<AdminApiModel>(result));
        }

        /// <summary>
        /// Удаляет администратора по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await adminService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}
