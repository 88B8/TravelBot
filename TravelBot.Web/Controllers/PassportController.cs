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
    /// CRUD контроллер по работе с <see cref="PassportApiModel"/>
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class PassportController : ControllerBase
    {
        private readonly IPassportService passportService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public PassportController(IPassportService passportService, IValidateService validateService, IMapper mapper)
        {
            this.passportService = passportService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает паспорт по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await passportService.GetById(id, cancellationToken);

            return Ok(mapper.Map<PassportApiModel>(result));
        }

        /// <summary>
        /// Получает список всех паспортов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<PassportApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await passportService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<PassportApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новый паспорт
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(PassportCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<PassportCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await passportService.Create(createModel, cancellationToken);

            return Ok(mapper.Map<PassportApiModel>(result));
        }

        /// <summary>
        /// Редактирует паспорт по идентификатору
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(PassportApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, PassportCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<PassportCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await passportService.Edit(id, createModel, cancellationToken);

            return Ok(mapper.Map<PassportApiModel>(result));
        }

        /// <summary>
        /// Удаляет паспорт по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await passportService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}