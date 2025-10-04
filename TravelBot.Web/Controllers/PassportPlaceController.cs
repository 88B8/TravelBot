//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using TravelBot.Services.Contracts.Models.CreateModels;
//using TravelBot.Services.Contracts.Services;
//using TravelBot.Web.Exceptions;
//using TravelBot.Web.Models.CreateApiModels;
//using TravelBot.Web.Models.ResponseApiModels;

//namespace TravelBot.Web.Controllers
//{
//    /// <summary>
//    /// CRUD контроллер по работе с <see cref="PassportPlaceApiModel"/>
//    /// </summary>
//    [ApiController]
//    [Route("Api/[controller]")]
//    public class PassportPlaceController : ControllerBase
//    {
//        private readonly IPassportPlaceService PassportPlaceService;
//        private readonly IValidateService validateService;
//        private readonly IMapper mapper;

//        /// <summary>
//        /// ctor
//        /// </summary>
//        public PassportPlaceController(IPassportPlaceService PassportPlaceService, IValidateService validateService, IMapper mapper)
//        {
//            this.PassportPlaceService = PassportPlaceService;
//            this.validateService = validateService;
//            this.mapper = mapper;
//        }

//        /// <summary>
//        /// Получает посещенное место по идентификатору
//        /// </summary>
//        [HttpGet("{id:guid}")]
//        [ProducesResponseType(typeof(PassportPlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
//        {
//            var result = await PassportPlaceService.GetById(id, cancellationToken);

//            return Ok(mapper.Map<PassportPlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Получает список всех посещенных мест
//        /// </summary>
//        [HttpGet]
//        [ProducesResponseType(typeof(IReadOnlyCollection<PassportPlaceApiModel>), StatusCodes.Status200OK)]
//        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
//        {
//            var result = await PassportPlaceService.GetAll(cancellationToken);

//            return Ok(mapper.Map<IReadOnlyCollection<PassportPlaceApiModel>>(result));
//        }

//        /// <summary>
//        /// Добавляет новое посещенное место
//        /// </summary>
//        [HttpPost]
//        [ProducesResponseType(typeof(PassportPlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
//        public async Task<IActionResult> Create(PassportPlaceCreateApiModel request, CancellationToken cancellationToken)
//        {
//            var createModel = mapper.Map<PassportPlaceCreateModel>(request);
//            await validateService.Validate(createModel, cancellationToken);
//            var result = await PassportPlaceService.Create(createModel, cancellationToken);

//            return Ok(mapper.Map<PassportPlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Редактирует посещенное место по идентификатору
//        /// </summary>
//        [HttpPut]
//        [ProducesResponseType(typeof(PassportPlaceApiModel), StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
//        public async Task<IActionResult> Edit(Guid id, PassportPlaceCreateApiModel request, CancellationToken cancellationToken)
//        {
//            var createModel = mapper.Map<PassportPlaceCreateModel>(request);
//            await validateService.Validate(createModel, cancellationToken);
//            var result = await PassportPlaceService.Edit(id, createModel, cancellationToken);

//            return Ok(mapper.Map<PassportPlaceApiModel>(result));
//        }

//        /// <summary>
//        /// Удаляет посещенное место по идентификатору
//        /// </summary>
//        [HttpDelete("{id:guid}")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
//        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
//        {
//            await PassportPlaceService.Delete(id, cancellationToken);

//            return Ok();
//        }
//    }
//}