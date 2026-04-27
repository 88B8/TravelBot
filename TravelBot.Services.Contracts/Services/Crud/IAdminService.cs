using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Contracts.Services.Crud;

/// <summary>
///     Сервис <see cref="AdminModel" />
/// </summary>
public interface IAdminService
{
    /// <summary>
    ///     Возвращает список всех <see cref="AdminModel" />
    /// </summary>
    Task<IReadOnlyCollection<AdminModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает <see cref="AdminModel" /> по идентификатору
    /// </summary>
    Task<AdminModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает <see cref="AdminModel" /> по логину
    /// </summary>
    Task<AdminModel> GetByLogin(string login, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Создает нового <see cref="AdminModel" />
    /// </summary>
    Task<AdminModel> Create(AdminCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактирует <see cref="AdminModel" /> по идентификатору
    /// </summary>
    Task<AdminModel> Edit(Guid id, AdminCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет <see cref="AdminModel" /> по идентификатору
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}