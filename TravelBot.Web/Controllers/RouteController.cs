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
    /// CRUD контроллер по работе с <see cref="RouteApiModel"/>
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService routeService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public RouteController(IRouteService routeService, IValidateService validateService, IMapper mapper)
        {
            this.routeService = routeService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает маршрут по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await routeService.GetById(id, cancellationToken);

            return Ok(mapper.Map<RouteApiModel>(result));
        }

        /// <summary>
        /// Получает список всех маршрутов
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<RouteApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await routeService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<RouteApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новый маршрут
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(RouteCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<RouteCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await routeService.Create(createModel, cancellationToken);

            return Ok(mapper.Map<RouteApiModel>(result));
        }

        /// <summary>
        /// Редактирует маршрут по идентификатору
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RouteApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, RouteCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<RouteCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await routeService.Edit(id, createModel, cancellationToken);

            return Ok(mapper.Map<RouteApiModel>(result));
        }

        /// <summary>
        /// Удаляет маршрут по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await routeService.Delete(id, cancellationToken);

            return Ok();
        }
    }
}