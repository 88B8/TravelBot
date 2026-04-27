using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.ReadRepositories;

/// <summary>
///     Репозиторий чтения <see cref="RoutePlace" />
/// </summary>
public interface IRoutePlaceReadRepository
{
    /// <summary>
    ///     Получить <see cref="RoutePlace" /> по идентификатору
    /// </summary>
    Task<RoutePlace?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="RoutePlace" /> по идентификатору маршрута
    /// </summary>
    Task<IReadOnlyCollection<RoutePlace>> GetByRouteId(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="RoutePlace" /> по идентификаторам мест
    /// </summary>
    Task<IReadOnlyCollection<RoutePlace>> GetByPlaceIds(IReadOnlyCollection<Guid> ids,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="RoutePlace" />
    /// </summary>
    Task<IReadOnlyCollection<RoutePlace>> GetAll(CancellationToken cancellationToken);
}