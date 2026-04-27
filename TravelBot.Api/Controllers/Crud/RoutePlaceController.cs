//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using TravelBot.Services.Contracts.Models.CreateModels;
//using TravelBot.Services.Contracts.Services;
//using TravelBot.Api.Exceptions;
//using TravelBot.Api.Models.CreateApiModels;
//using TravelBot.Api.Models.ResponseApiModels;

//namespace TravelBot.Api.Controllers
//{
//    /// <summary>
//    /// CRUD контроллер по работе с <see cref="RoutePlaceApiModel"/>
//    /// </summary>
//    [ApiController]
//    [Route("Api/[controller]")]
//    public class RoutePlaceController : ControllerBase
//    {
//        private readonly IRoutePlaceService routePlaceService;
//        private readonly IValidateService validateService;
//        private readonly IMapper mapper;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        public RoutePlaceController(IRoutePlaceService routePlaceService, IValidateService validateService, IMapper mapper)
//        {
//            this.routePlaceService = routePlaceService;
//            this.validateService = validateService;
//            this.mapper = mapper;
//        }

//        /// <summary>
//        /// Получает маршрут-место по идентификатору
//        /// </summary>
//        [HttpGet("{id:guid}")]
//        [ProducesResponseType(typeof(RoutePlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
//        {
//            var result = await routePlaceService.GetById(id, cancellationToken);

//            return Ok(mapper.Map<RoutePlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Получает список всех маршрут-место
//        /// </summary>
//        [HttpGet]
//        [ProducesResponseType(typeof(IReadOnlyCollection<RoutePlaceApiModel>), StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
//        {
//            var result = await routePlaceService.GetAll(cancellationToken);

//            return Ok(mapper.Map<IReadOnlyCollection<RoutePlaceApiModel>>(result));
//        }

//        /// <summary>
//        /// Добавляет новое маршрут-место
//        /// </summary>
//        [HttpPost]
//        [ProducesResponseType(typeof(RoutePlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
//        public async Task<IActionResult> Create(RoutePlaceCreateApiModel request, CancellationToken cancellationToken)
//        {
//            var createModel = mapper.Map<RoutePlaceCreateModel>(request);
//            await validateService.Validate(createModel, cancellationToken);
//            var result = await routePlaceService.Create(createModel, cancellationToken);

//            return Ok(mapper.Map<RoutePlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Редактирует маршрут-место по идентификатору
//        /// </summary>
//        [HttpPut]
//        [ProducesResponseType(typeof(RoutePlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
//        public async Task<IActionResult> Edit(Guid id, RoutePlaceCreateApiModel request, CancellationToken cancellationToken)
//        {
//            var createModel = mapper.Map<RoutePlaceCreateModel>(request);
//            await validateService.Validate(createModel, cancellationToken);
//            var result = await routePlaceService.Edit(id, createModel, cancellationToken);

//            return Ok(mapper.Map<RoutePlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Удаляет маршрут-место по идентификатору
//        /// </summary>
//        [HttpDelete("{id:guid}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
//        {
//            await routePlaceService.Delete(id, cancellationToken);

//            return Ok();
//        }
//    }
//}

