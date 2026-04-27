using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;

namespace TravelBot.Repositories.Contracts.ReadRepositories;

/// <summary>
///     Репозиторий чтения <see cref="User" />
/// </summary>
public interface IUserReadRepository
{
    /// <summary>
    ///     Получить <see cref="UserDbModel" /> по идентификатору
    /// </summary>
    Task<UserDbModel?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить <see cref="User" /> по идентификатору без связанных сущностей
    /// </summary>
    Task<User?> GetByIdRaw(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить <see cref="User" /> по телеграм идентификатору
    /// </summary>
    Task<UserDbModel?> GetByTelegramId(long id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="UserDbModel" />
    /// </summary>
    Task<IReadOnlyCollection<UserDbModel>> GetAll(CancellationToken cancellationToken);
}