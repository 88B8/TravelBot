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
    /// CRUD контроллер по работе с <see cref="CategoryApiModel"/>
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("Api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IValidateService validateService;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public CategoryController(ICategoryService categoryService, IValidateService validateService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.validateService = validateService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получает категорию по идентификатору
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await categoryService.GetById(id, cancellationToken);

            return Ok(mapper.Map<CategoryApiModel>(result));
        }

        /// <summary>
        /// Получает список всех категорий
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CategoryApiModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await categoryService.GetAll(cancellationToken);

            return Ok(mapper.Map<IReadOnlyCollection<CategoryApiModel>>(result));
        }

        /// <summary>
        /// Добавляет новую категорию
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(CategoryCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<CategoryCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await categoryService.Create(createModel, cancellationToken);

            return Ok(mapper.Map<CategoryApiModel>(result));
        }

        /// <summary>
        /// Редактирует категорию по идентификатору
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(CategoryApiModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiValidationExceptionDetail), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Edit(Guid id, CategoryCreateApiModel request, CancellationToken cancellationToken)
        {
            var createModel = mapper.Map<CategoryCreateModel>(request);
            await validateService.Validate(createModel, cancellationToken);
            var result = await categoryService.Edit(id, createModel, cancellationToken);

            return Ok(mapper.Map<CategoryApiModel>(result));
        }

        /// <summary>
        /// Удаляет категорию по идентификатору
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await categoryService.Delete(id, cancellationToken);
            
            return Ok();
        }
    }
}