using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="PlaceModel"/>
    /// </summary>
    public interface IPlaceService
    {
        /// <summary>
        /// Возвращает список всех <see cref="PlaceModel"/>
        /// </summary>
        Task<IReadOnlyCollection<PlaceModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает <see cref="PlaceModel"/> по идентификатору
        /// </summary>
        Task<PlaceModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PlaceModel"/>
        /// </summary>
        Task<PlaceModel> Create(PlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует <see cref="PlaceModel"/> по идентификатору
        /// </summary>
        Task<PlaceModel> Edit(Guid id, PlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="PlaceModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
