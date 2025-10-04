using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="RoutePlace"/>
    /// </summary>
    public interface IRoutePlaceWriteRepository : IDbWriter<RoutePlace>
    {

    }
}
