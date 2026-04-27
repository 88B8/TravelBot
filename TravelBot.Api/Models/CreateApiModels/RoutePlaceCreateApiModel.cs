using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания и редактирования маршрута-места
/// </summary>
public class RoutePlaceCreateApiModel : RoutePlaceBaseApiModel
{
    /// <summary>
    ///     Идентификатор места
    /// </summary>
    public Guid PlaceId { get; set; }
}