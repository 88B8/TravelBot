using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания и редактирования посещенного места
/// </summary>
public class PassportPlaceCreateApiModel : PassportPlaceBaseApiModel
{
    /// <summary>
    ///     Идентификатор места
    /// </summary>
    public Guid PlaceId { get; set; }
}