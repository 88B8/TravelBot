using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания и редактирования маршрута
/// </summary>
public class RouteCreateApiModel : RouteBaseApiModel
{
    /// <summary>
    ///     Места
    /// </summary>
    public List<Guid> PlaceIds { get; set; } = new();
}