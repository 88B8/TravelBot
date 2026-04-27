using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Contracts.Services.Crud;

/// <summary>
///     Сервис <see cref="PassportModel" />
/// </summary>
public interface IPassportService
{
    /// <summary>
    ///     Возвращает список всех <see cref="PassportModel" />
    /// </summary>
    Task<IReadOnlyCollection<PassportModel>> GetAll(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает <see cref="PassportModel" /> по идентификатору
    /// </summary>
    Task<PassportModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый <see cref="PassportModel" />
    /// </summary>
    Task<PassportModel> Create(PassportCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактирует <see cref="PassportModel" /> по идентификатору
    /// </summary>
    Task<PassportModel> Edit(Guid id, PassportCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет <see cref="PassportModel" /> по идентификатору
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}