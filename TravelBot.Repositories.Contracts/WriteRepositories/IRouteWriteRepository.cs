using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Route"/>
    /// </summary>
    public interface IRouteWriteRepository : IDbWriter<Route>
    {

    }
}