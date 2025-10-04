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
    /// <inheritdoc cref="IPassportService"/>
    public class PassportService : IPassportService, IServiceAnchor
    {
        private readonly IPassportReadRepository passportReadRepository;
        private readonly IPassportWriteRepository passportWriteRepository;
        private readonly IPlaceReadRepository placeReadRepository;
        private readonly IPassportPlaceReadRepository passportPlaceReadRepository;
        private readonly IPassportPlaceWriteRepository passportPlaceWriteRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public PassportService(IPassportReadRepository passportReadRepository, IPassportWriteRepository passportWriteRepository, IPlaceReadRepository placeReadRepository, IPassportPlaceReadRepository passportPlaceReadRepository, IPassportPlaceWriteRepository passportPlaceWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.passportReadRepository = passportReadRepository;
            this.passportWriteRepository = passportWriteRepository;
            this.placeReadRepository = placeReadRepository;
            this.passportPlaceReadRepository = passportPlaceReadRepository;
            this.passportPlaceWriteRepository = passportPlaceWriteRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        async Task<PassportModel> IPassportService.Create(PassportCreateModel model, CancellationToken cancellationToken)
        {
            var places = await placeReadRepository.GetByIds(model.PlaceIds, cancellationToken);
            var placeIds = places.Select(x => x.Id);

            var missingIds = model.PlaceIds.Except(placeIds).ToList();
            if (missingIds.Any())
            {
                throw new TravelBotNotFoundException(
                    $"Не удалось найти места с идентификатором {string.Join(", ", missingIds)}");
            }

            var item = new Passport
            {
                Id = Guid.NewGuid(),
            };

            passportWriteRepository.Add(item);

            foreach (var placeId in model.PlaceIds)
            {
                var passportPlace = new PassportPlace
                {
                    PlaceId = placeId,
                    PassportId = item.Id,
                };
                passportPlaceWriteRepository.Add(passportPlace);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var passport = mapper.Map<PassportModel>(item);
            passport.Places = mapper.Map<List<PlaceModel>>(places);

            return passport;
        }

        async Task IPassportService.Delete(Guid id, CancellationToken cancellationToken)
        {
            var item = await passportReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти паспорт с идентификатором {id}");

            passportWriteRepository.Delete(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        async Task<PassportModel> IPassportService.Edit(Guid id, PassportCreateModel model, CancellationToken cancellationToken)
        {
            var item = await passportReadRepository.GetByIdRaw(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти паспорт с идентификатором {id}");

            var places = await placeReadRepository.GetByIds(model.PlaceIds, cancellationToken);
            var placeIds = places.Select(x => x.Id);
            var missingIds = model.PlaceIds.Except(placeIds).ToList();

            if (missingIds.Any())
            {
                throw new TravelBotNotFoundException(
                    $"Не удалось найти места с идентификатором {string.Join(", ", missingIds)}");
            }

            var passportPlaces = await passportPlaceReadRepository.GetByPassportId(id, cancellationToken);
            var passportPlaceIds = passportPlaces.Select(x => x.PlaceId).ToList();
            var placeToDeleteIds = passportPlaceIds
                .Except(model.PlaceIds)
                .ToList();
            var placesToDelete = await passportPlaceReadRepository.GetByPlaceIds(placeToDeleteIds, cancellationToken);
            var placeToAddIds = model.PlaceIds
                .Except(passportPlaceIds);

            foreach (var placeToDelete in placesToDelete)
            {
                passportPlaceWriteRepository.Delete(placeToDelete);
            }

            foreach (var placeToAddId in placeToAddIds)
            {
                var passportPlace = new PassportPlace
                {
                    PlaceId = placeToAddId,
                    PassportId = id,
                };
                passportPlaceWriteRepository.Add(passportPlace);
            }

            mapper.Map(model, item);

            passportWriteRepository.Update(item);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<PassportModel>(item);
            result.Places = mapper.Map<List<PlaceModel>>(places);

            return result;
        }

        async Task<IReadOnlyCollection<PassportModel>> IPassportService.GetAll(CancellationToken cancellationToken)
        {
            var items = await passportReadRepository.GetAll(cancellationToken);

            return mapper.Map<IReadOnlyCollection<PassportModel>>(items);
        }

        async Task<PassportModel> IPassportService.GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await passportReadRepository.GetById(id, cancellationToken)
                       ?? throw new TravelBotNotFoundException($"Не удалось найти паспорт с идентификатором {id}");

            return mapper.Map<PassportModel>(item);
        }
    }
}