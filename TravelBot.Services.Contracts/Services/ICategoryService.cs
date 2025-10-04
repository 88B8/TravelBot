using TravelBot.Services.Contracts.Models.RequestModels;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис <see cref="CategoryModel"/>
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает список всех <see cref="CategoryModel"/>
        /// </summary>
        Task<IReadOnlyCollection<CategoryModel>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает <see cref="CategoryModel"/> по идентификатору
        /// </summary>
        Task<CategoryModel> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет новый <see cref="CategoryModel"/>
        /// </summary>
        Task<CategoryModel> Create(CategoryCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Редактирует <see cref="CategoryModel"/> по идентификатору
        /// </summary>
        Task<CategoryModel> Edit(Guid id, CategoryCreateModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет <see cref="CategoryModel"/> по идентификатору
        /// </summary>
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
