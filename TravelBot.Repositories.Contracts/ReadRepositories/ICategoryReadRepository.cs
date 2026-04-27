using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.ReadRepositories;

/// <summary>
///     Репозиторий чтения <see cref="Category" />
/// </summary>
public interface ICategoryReadRepository
{
    /// <summary>
    ///     Получить <see cref="Category" /> по идентификатору
    /// </summary>
    Task<Category?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список всех <see cref="Category" />
    /// </summary>
    Task<IReadOnlyCollection<Category>> GetAll(CancellationToken cancellationToken);
}