using TravelBot.Services.Contracts.Models.CreateModels;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Contracts.Services.Crud;

/// <summary>
///     Сервис <see cref="RouteModel" />
/// </summary>
public interface IRouteService
{
    /// <summary>
    ///     Возвращает список всех <see cref="RouteModel" />
    /// </summary>
    Task<IReadOnlyCollection<RouteModel>> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    ///     Возвращает список всех активных <see cref="RouteModel" />
    /// </summary>
    Task<IReadOnlyCollection<RouteModel>> GetAllActive(CancellationToken cancellationToken);

    /// <summary>
    ///     Возвращает <see cref="RouteModel" /> по идентификатору
    /// </summary>
    Task<RouteModel> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Добавляет новый <see cref="RouteModel" />
    /// </summary>
    Task<RouteModel> Create(RouteCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактирует <see cref="RouteModel" /> по идентификатору
    /// </summary>
    Task<RouteModel> Edit(Guid id, RouteCreateModel model, CancellationToken cancellationToken);

    /// <summary>
    ///     Удаляет <see cref="RouteModel" /> по идентификатору
    /// </summary>
    Task Delete(Guid id, CancellationToken cancellationToken);
}