using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.ReadRepositories;

/// <summary>
///     Репозиторий чтения <see cref="Admin" />
/// </summary>
public interface IAdminReadRepository
{
    /// <summary>
    ///     Получает <see cref="Admin" /> по идентификатору
    /// </summary>
    Task<Admin?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получает список всех <see cref="Admin" />
    /// </summary>
    Task<IReadOnlyCollection<Admin>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    ///     Получает <see cref="Admin" /> по логину
    /// </summary>
    Task<Admin?> GetByLogin(string login, CancellationToken cancellationToken);
}