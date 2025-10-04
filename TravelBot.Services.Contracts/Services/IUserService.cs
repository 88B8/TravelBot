using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="UserModel"/>
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Возвращает список всех <see cref="UserModel"/>
        /// </summary>
        Task<IReadOnlyCollection<UserModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает <see cref="UserModel"/> по идентификатору
        /// </summary>
        Task<UserModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="UserModel"/>
        /// </summary>
        Task<UserModel> Create(UserCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует <see cref="UserModel"/> по идентификатору
        /// </summary>
        Task<UserModel> Edit(Guid id, UserCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="UserModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
