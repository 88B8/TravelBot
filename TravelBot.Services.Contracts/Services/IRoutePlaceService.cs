using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="RoutePlaceModel"/>
    /// </summary>
    public interface IRoutePlaceService
    {
        /// <summary>
        /// Возвращает список всех <see cref="RoutePlaceModel"/>
        /// </summary>
        Task<IReadOnlyCollection<RoutePlaceModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает <see cref="RoutePlaceModel"/> по идентификатору
        /// </summary>
        Task<RoutePlaceModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="RoutePlaceModel"/>
        /// </summary>
        Task<RoutePlaceModel> Create(RoutePlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует <see cref="RoutePlaceModel"/> по идентификатору
        /// </summary>
        Task<RoutePlaceModel> Edit(Guid id, RoutePlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="RoutePlaceModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
