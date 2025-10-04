using AutoMapper;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Services;

namespace TravelBot.Services.Services
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService, IServiceAnchor
    {
        private readonly ICategoryReadRepository categoryReadRepository;
        private readonly ICategoryWriteRepository categoryWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.categoryReadRepository = categoryReadRepository;
            this.categoryWriteRepository = categoryWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<CategoryModel> ICategoryService.Create(CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<Category>(model);

            categoryWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<CategoryModel>(item);
        }

        async Task ICategoryService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await categoryReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти категорию с идентификатором {id}");

            categoryWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<CategoryModel> ICategoryService.Edit(Guid id, CategoryCreateModel model, CancellationToken cancellationToken)
        {
            var item = await categoryReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти категорию с идентификатором {id}");

            mapper.Map(model, item);

            categoryWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await categoryReadRepository.GetById(id, cancellationToken);

            return mapper.Map<CategoryModel>(updatedEntity);
        }

        async Task<IReadOnlyCollection<CategoryModel>> ICategoryService.GetAll(CancellationToken cancellationToken)
        {
            var items = await categoryReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<CategoryModel>>(items);
        }

        async Task<CategoryModel> ICategoryService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await categoryReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти категорию с идентификатором {id}");

            return mapper.Map<CategoryModel>(item);
        }
    }
}
