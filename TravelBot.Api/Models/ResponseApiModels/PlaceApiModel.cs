using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.ResponseApiModels;

/// <summary>
///     API модель места
/// </summary>
public class PlaceApiModel : PlaceBaseApiModel
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     Категория
    /// </summary>
    public CategoryApiModel Category { get; set; } = null!;
}