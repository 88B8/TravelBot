using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.ResponseApiModels;

/// <summary>
///     API модель паспорта
/// </summary>
public class PassportApiModel : PassportBaseApiModel
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Навигационное свойство посещенных мест
    /// </summary>
    public List<PlaceApiModel> Places { get; set; } = new();
}