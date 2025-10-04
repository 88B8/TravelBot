using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories
{
    /// <summary>
    /// Репозиторий записи сущности <see cref="Admin"/>
    /// </summary>
    public interface IAdminWriteRepository : IDbWriter<Admin>
    {

    }
}
