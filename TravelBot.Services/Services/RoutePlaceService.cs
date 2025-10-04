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
    /// <inheritdoc cref="IRoutePlaceService"/>
    public class RoutePlaceService : IRoutePlaceService, IServiceAnchor
    {
        private readonly IRoutePlaceReadRepository routePlaceReadRepository;
        private readonly IRoutePlaceWriteRepository routePlaceWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public RoutePlaceService(IRoutePlaceReadRepository routePlaceReadRepository, IRoutePlaceWriteRepository routePlaceWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.routePlaceReadRepository = routePlaceReadRepository;
            this.routePlaceWriteRepository = routePlaceWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<RoutePlaceModel> IRoutePlaceService.Create(RoutePlaceCreateModel model, CancellationToken cancellationToken)
        {
            var item = mapper.Map<RoutePlace>(model);

            routePlaceWriteRepository.Add(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<RoutePlaceModel>(item);
        }

        async Task IRoutePlaceService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await routePlaceReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут-место с идентификатором {id}");

            routePlaceWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<RoutePlaceModel> IRoutePlaceService.Edit(Guid id, RoutePlaceCreateModel model, CancellationToken cancellationToken)
        {
            var item = await routePlaceReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут-место с идентификатором {id}");

            mapper.Map(model, item);

            routePlaceWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var updatedEntity = await routePlaceReadRepository.GetById(id, cancellationToken);

            return mapper.Map<RoutePlaceModel>(updatedEntity);
        }

        async Task<IReadOnlyCollection<RoutePlaceModel>> IRoutePlaceService.GetAll(CancellationToken cancellationToken)
        {
            var items = await routePlaceReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<RoutePlaceModel>>(items);
        }

        async Task<RoutePlaceModel> IRoutePlaceService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await routePlaceReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут-место с идентификатором {id}");

            return mapper.Map<RoutePlaceModel>(item);
        }
    }
}
