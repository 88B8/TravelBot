using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels;

/// <summary>
///     Модель создания и редактирования маршрута
/// </summary>
public class RouteCreateModel : RouteBaseModel
{
    /// <summary>
    ///     Места
    /// </summary>
    public List<Guid> PlaceIds { get; set; } = null!;
}