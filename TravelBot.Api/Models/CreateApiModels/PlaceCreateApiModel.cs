using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания и редактирования места
/// </summary>
public class PlaceCreateApiModel : PlaceBaseApiModel
{
    /// <summary>
    ///     Идентификатор категории
    /// </summary>
    public Guid CategoryId { get; set; }
}