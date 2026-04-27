using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories;

/// <summary>
///     Репозиторий записи сущности <see cref="User" />
/// </summary>
public interface IUserWriteRepository : IDbWriter<User>
{
}