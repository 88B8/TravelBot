using TravelBot.Context.Contracts;
using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.WriteRepositories;

/// <summary>
///     Репозиторий записи сущности <see cref="PassportPlace" />
/// </summary>
public interface IPassportPlaceWriteRepository : IDbWriter<PassportPlace>
{
}