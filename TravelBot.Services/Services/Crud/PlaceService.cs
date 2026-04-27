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

/// <inheritdoc cref="IPlaceService" />
public class PlaceService : IPlaceService, IServiceAnchor
{
    private readonly ICategoryReadRepository categoryReadRepository;
    private readonly IMapper mapper;
    private readonly IPlaceReadRepository placeReadRepository;
    private readonly IPlaceWriteRepository placeWriteRepository;
    private readonly IUnitOfWork unitOfWork;

    /// <summary>
    ///     ctor
    /// </summary>
    public PlaceService(IPlaceReadRepository placeReadRepository, IPlaceWriteRepository placeWriteRepository,
        ICategoryReadRepository categoryReadRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.placeReadRepository = placeReadRepository;
        this.placeWriteRepository = placeWriteRepository;
        this.categoryReadRepository = categoryReadRepository;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    async Task<PlaceModel> IPlaceService.Create(PlaceCreateModel model, CancellationToken cancellationToken)
    {
        var item = mapper.Map<Place>(model);

        placeWriteRepository.Add(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<PlaceModel>(item);
    }

    async Task IPlaceService.Delete(Guid id, CancellationToken cancellationToken)
    {
        var item = await placeReadRepository.GetByIdRaw(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

        placeWriteRepository.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task<PlaceModel> IPlaceService.Edit(Guid id, PlaceCreateModel model, CancellationToken cancellationToken)
    {
        var item = await placeReadRepository.GetByIdRaw(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");
        var category = await categoryReadRepository.GetById(model.CategoryId, cancellationToken)
                       ?? throw new TravelBotNotFoundException(
                           $"Не удалось найти категорию с идентификатором {model.CategoryId}");

        mapper.Map(model, item);

        placeWriteRepository.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        item.Category = category;
        var result = mapper.Map<PlaceModel>(item);

        return result;
    }

    async Task<IReadOnlyCollection<PlaceModel>> IPlaceService.GetAll(CancellationToken cancellationToken)
    {
        var items = await placeReadRepository.GetAll(cancellationToken);

        return mapper.Map<IReadOnlyCollection<PlaceModel>>(items);
    }

    async Task<PlaceModel> IPlaceService.GetById(Guid id, CancellationToken cancellationToken)
    {
        var item = await placeReadRepository.GetById(id, cancellationToken)
                   ?? throw new TravelBotNotFoundException($"Не удалось найти место с идентификатором {id}");

        return mapper.Map<PlaceModel>(item);
    }
}