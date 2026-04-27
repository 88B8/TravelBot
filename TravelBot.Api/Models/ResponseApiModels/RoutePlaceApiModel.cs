using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.ResponseApiModels;

/// <summary>
///     API модель маршрута-места
/// </summary>
public class RoutePlaceApiModel : RoutePlaceBaseApiModel
{
    /// <summary>
    ///     Место
    /// </summary>
    public PlaceApiModel Place { get; set; } = null!;
}