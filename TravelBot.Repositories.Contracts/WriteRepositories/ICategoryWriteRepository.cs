using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Category"/>
    /// </summary>
    public interface ICategoryWriteRepository : IDbWriter<Category>
    {

    }
}
