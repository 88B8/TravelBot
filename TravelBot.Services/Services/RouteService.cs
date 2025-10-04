using AutoMapper;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Entities.Enums;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.Enums;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Services;

namespace TravelBot.Services.Services
{
    /// <inheritdoc cref="IRouteService"/>
    public class RouteService : IRouteService, IServiceAnchor
    {
        private readonly IRouteReadRepository routeReadRepository;
        private readonly IRouteWriteRepository routeWriteRepository;
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IRoutePlaceReadRepository routePlaceReadRepository;
        private readonly IRoutePlaceWriteRepository routePlaceWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public RouteService(IRouteReadRepository routeReadRepository, IRouteWriteRepository routeWriteRepository, IPlaceReadRepository placeReadRepository, IRoutePlaceReadRepository routePlaceReadRepository, IRoutePlaceWriteRepository routePlaceWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.routeReadRepository = routeReadRepository;
            this.routeWriteRepository = routeWriteRepository;
            this.placeReadRepository = placeReadRepository;
            this.routePlaceReadRepository = routePlaceReadRepository;
            this.routePlaceWriteRepository = routePlaceWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<RouteModel> IRouteService.Create(RouteCreateModel model, CancellationToken cancellationToken)
        {
            var places = await placeReadRepository.GetByIds(model.PlaceIds, cancellationToken);
            var placeIds = places.Select(x => x.Id);

            var missingIds = model.PlaceIds.Except(placeIds).ToList();
            if (missingIds.Any())
            {
                throw new TravelBotNotFoundException(
                    $"Не удалось найти места с идентификатором {string.Join(", ", missingIds)}");
            }

            var item = new Route
            {
                AverageTime = model.AverageTime,
                Budget = model.Budget,
                ReasonToVisit = model.ReasonToVisit,
                StartPoint = model.StartPoint,
            };

            routeWriteRepository.Add(item);

            foreach (var placeId in model.PlaceIds)
            {
                var routePlace = new RoutePlace
                {
                    PlaceId = placeId,
                    RouteId = item.Id,
                };
                routePlaceWriteRepository.Add(routePlace);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var route = mapper.Map<RouteModel>(item);
            route.Places = mapper.Map<List<PlaceModel>>(places);
            route.Season = model.Season;

            return route;
        }

        async Task IRouteService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await routeReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут с идентификатором {id}");

            routeWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<RouteModel> IRouteService.Edit(Guid id, RouteCreateModel model, CancellationToken cancellationToken)
        {
            var item = await routeReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут с идентификатором {id}");

            var places = await placeReadRepository.GetByIds(model.PlaceIds, cancellationToken);
            var placeIds = places.Select(x => x.Id);
            var missingIds = model.PlaceIds.Except(placeIds).ToList();

            if (missingIds.Any())
            {
                throw new TravelBotNotFoundException(
                    $"Не удалось найти места с идентификатором {string.Join(", ", missingIds)}");
            }

            var routePlaces = await routePlaceReadRepository.GetByRouteId(id, cancellationToken);
            var routePlaceIds = routePlaces.Select(x => x.Id).ToList();
            var placeToDeleteIds = routePlaceIds
                .Except(model.PlaceIds)
                .ToList();
            var placesToDelete = await routePlaceReadRepository.GetByPlaceIds(placeToDeleteIds, cancellationToken);
            var placeToAddIds = model.PlaceIds
                .Except(routePlaceIds);

            foreach (var placeToDelete in placesToDelete)
            {
                routePlaceWriteRepository.Delete(placeToDelete);
            }

            foreach (var placeToAddId in placeToAddIds)
            {
                var routePlace = new RoutePlace
                {
                    PlaceId = placeToAddId,
                    RouteId = id,
                };
                routePlaceWriteRepository.Add(routePlace);
            }

            mapper.Map(model, item);

            routeWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<RouteModel>(item);
            result.Places = mapper.Map<List<PlaceModel>>(places);

            return result;
        }

        async Task<IReadOnlyCollection<RouteModel>> IRouteService.GetAll(CancellationToken cancellationToken)
        {
            var items = await routeReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<RouteModel>>(items);
        }

        async Task<RouteModel> IRouteService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await routeReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти маршрут с идентификатором {id}");

            return mapper.Map<RouteModel>(item);
        }
    }
}
