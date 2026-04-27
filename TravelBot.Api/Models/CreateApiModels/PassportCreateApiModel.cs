using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания и редактирования паспорта
/// </summary>
public class PassportCreateApiModel : PassportBaseApiModel
{
    /// <summary>
    ///     Посещенные места
    /// </summary>
    public List<Guid> PlaceIds { get; set; } = new();
}