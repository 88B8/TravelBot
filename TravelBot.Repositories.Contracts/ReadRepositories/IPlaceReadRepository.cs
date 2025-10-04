using TravelBot.Entities;
using TravelBot.Repositories.Contracts.Models;

namespace TravelBot.Repositories.Contracts.ReadRepositories
{
    /// <summary>
    /// Репозиторий чтения <see cref="Place"/>
    /// </summary>
    public interface IPlaceReadRepository
    {
        /// <summary>
        /// Получить <see cref="PlaceDbModel"/> по идентификатору
        /// </summary>
        Task<PlaceDbModel?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить <see cref="Place"/> по идентификатору без связанных сущностей
        /// </summary>
        Task<Place?> GetByIdRaw(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает <see cref="PlaceDbModel"/> по идентификатором
        /// </summary>
        Task<IReadOnlyCollection<PlaceDbModel>> GetByIds(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список всех <see cref="PlaceDbModel"/>
        /// </summary>
        Task<IReadOnlyCollection<PlaceDbModel>> GetAll(CancellationToken cancellationToken);
    }
}