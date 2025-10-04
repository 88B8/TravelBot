using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;

namespace TravelBot.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения <see cref="Passport"/>
    /// </summary>
    public interface IPassportReadRepository
    {
        /// <summary>
        /// Получить <see cref="PassportDbModel"/> по идентификатору
        /// </summary>
        Task<PassportDbModel?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Passport"/> по идентификатору без связанных сущностей
        /// </summary>
        Task<Passport?> GetByIdRaw(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="PassportDbModel"/>
        /// </summary>
        Task<IReadOnlyCollection<PassportDbModel>> GetAll(CancellationToken cancellationToken);
    }
}