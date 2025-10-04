using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Place"/>
    /// </summary>
    public interface IPlaceWriteRepository : IDbWriter<Place>
    {

    }
}
