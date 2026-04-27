using AutoMapper;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Contracts.WriteRepositories;
using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Services.Crud;

namespace TravelBot.Services.Services.Crud;

/// <inheritdoc cref="IPassportPlaceService" />
public class PassportPlaceService : IPassportPlaceService, IServiceAnchor
{
    private readonly IMapper mapper;
    private readonly IPassportPlaceReadRepository passportPlaceReadRepository;
    private readonly IPassportPlaceWriteRepository passportPlaceWriteRepository;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    ///     ctor
    /// </summary>
    public PassportPlaceService(IPassportPlaceReadRepository passportPlaceReadRepository,
        IPassportPlaceWriteRepository passportPlaceWriteRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.passportPlaceReadRepository = passportPlaceReadRepository;
        this.passportPlaceWriteRepository = passportPlaceWriteRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    async Task<PassportPlaceModel> IPassportPlaceService.Create(PassportPlaceCreateModel model,
        CancellationToken cancellationToken)
    {
        var item = mapper.Map<PassportPlace>(model);

        passportPlaceWriteRepository.Add(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<PassportPlaceModel>(item);
    }

    async Task IPassportPlaceService.Delete(Guid id, CancellationToken cancellationToken)
    {
        var item = await passportPlaceReadRepository.GetById(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

        passportPlaceWriteRepository.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task<PassportPlaceModel> IPassportPlaceService.Edit(Guid id, PassportPlaceCreateModel model,
        CancellationToken cancellationToken)
    {
        var item = await passportPlaceReadRepository.GetById(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

        mapper.Map(model, item);

        passportPlaceWriteRepository.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedEntity = await passportPlaceReadRepository.GetById(id, cancellationToken);

        return mapper.Map<PassportPlaceModel>(updatedEntity);
    }

    async Task<IReadOnlyCollection<PassportPlaceModel>> IPassportPlaceService.GetAll(
        CancellationToken cancellationToken)
    {
        var items = await passportPlaceReadRepository.GetAll(cancellationToken);

        return mapper.Map<IReadOnlyCollection<PassportPlaceModel>>(items);
    }

    async Task<PassportPlaceModel> IPassportPlaceService.GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await passportPlaceReadRepository.GetById(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

        return mapper.Map<PassportPlaceModel>(item);
    }
}