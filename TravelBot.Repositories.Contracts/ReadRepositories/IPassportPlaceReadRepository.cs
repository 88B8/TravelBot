using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения <see cref="PassportPlace"/>
    /// </summary>
    public interface IPassportPlaceReadRepository
    {
        /// <summary>
        /// Получить <see cref="PassportPlace"/> по идентификатору
        /// </summary>
        Task<PassportPlace?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="PassportPlace"/> по идентификатору паспорта
        /// </summary>
        Task<IReadOnlyCollection<PassportPlace>> GetByPassportId(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="PassportPlace"/> по идентификаторам мест
        /// </summary>
        Task<IReadOnlyCollection<PassportPlace>> GetByPlaceIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="PassportPlace"/>
        /// </summary>
        Task<IReadOnlyCollection<PassportPlace>> GetAll(CancellationToken cancellationToken);
    }
}