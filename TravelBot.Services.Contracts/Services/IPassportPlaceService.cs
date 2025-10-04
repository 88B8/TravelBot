using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="PassportPlaceModel"/>
    /// </summary>
    public interface IPassportPlaceService
    {
        /// <summary>
        /// Возвращает список всех <see cref="PassportPlaceModel"/>
        /// </summary>
        Task<IReadOnlyCollection<PassportPlaceModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает <see cref="PassportPlaceModel"/> по идентификатору
        /// </summary>
        Task<PassportPlaceModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="PassportPlaceModel"/>
        /// </summary>
        Task<PassportPlaceModel> Create(PassportPlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует <see cref="PassportPlaceModel"/> по идентификатору
        /// </summary>
        Task<PassportPlaceModel> Edit(Guid id, PassportPlaceCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="PassportPlaceModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
