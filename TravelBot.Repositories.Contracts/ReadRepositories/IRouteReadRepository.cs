using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;

namespace TravelBot.Repositories.Contracts.ReadRepositories;

/// <summary>
///     Репозиторий чтения <see cref="Route" />
/// </summary>
public interface IRouteReadRepository
{
    /// <summary>
    ///     Получить <see cref="RouteDbModel" /> по идентификатору
    /// </summary>
    Task<RouteDbModel?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить <see cref="Route" /> по идентификатору без связанных сущностей
    /// </summary>
    Task<Route?> GetByIdRaw(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="RouteDbModel" />
    /// </summary>
    Task<IReadOnlyCollection<RouteDbModel>> GetAll(CancellationToken cancellationToken);
}