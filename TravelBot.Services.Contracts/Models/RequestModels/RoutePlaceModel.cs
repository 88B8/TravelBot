using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.RequestModels;

/// <summary>
///     Модель маршрут-место
/// </summary>
public class RoutePlaceModel : RouteBaseModel
{
    /// <summary>
    ///     Место
    /// </summary>
    public PlaceModel Place { get; set; } = null!;
}