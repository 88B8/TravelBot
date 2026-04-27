using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TravelBot.Context.Contracts;
using TravelBot.Entities;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Repositories.Specs;

namespace TravelBot.Repositories.ReadRepositories;

/// <inheritdoc cref="IRoutePlaceReadRepository" />
public class RoutePlaceReadRepository : IRoutePlaceReadRepository, IRepositoryAnchor
{
    private readonly IReader reader;

    /// <summary>
    ///     ctor
    /// </summary>
    public RoutePlaceReadRepository(IReader reader)
    {
        this.reader = reader;
    }

    Task<IReadOnlyCollection<RoutePlace>> IRoutePlaceReadRepository.GetAll(CancellationToken cancellationToken)
    {
        return reader.Read<RoutePlace>()
            .NotDeletedAt()
            .ToReadOnlyCollectionAsync(cancellationToken);
    }

    Task<RoutePlace?> IRoutePlaceReadRepository.GetById(Guid id, CancellationToken cancellationToken)
    {
        return reader.Read<RoutePlace>()
            .NotDeletedAt()
            .ById(id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    Task<IReadOnlyCollection<RoutePlace>> IRoutePlaceReadRepository.GetByPlaceIds(IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken)
    {
        return reader.Read<RoutePlace>()
            .NotDeletedAt()
            .Where(ByPlaceIds(ids))
            .ToReadOnlyCollectionAsync(cancellationToken);
    }

    Task<IReadOnlyCollection<RoutePlace>> IRoutePlaceReadRepository.GetByRouteId(Guid id,
        CancellationToken cancellationToken)
    {
        return reader.Read<RoutePlace>()
            .NotDeletedAt()
            .Where(x => x.RouteId == id)
            .ToReadOnlyCollectionAsync(cancellationToken);
    }

    /// <summary>
    ///     Метод для вычисления идентификатора места
    /// </summary>
    private static Expression<Func<RoutePlace, bool>> ByPlaceIds(IReadOnlyCollection<Guid> ids)
    {
        var quantity = ids.Count;
        return quantity switch
        {
            0 => x => false,
            1 => x => x.PlaceId == ids.First(),
            _ => x => ids.Contains(x.PlaceId)
        };
    }
}