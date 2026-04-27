using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.ResponseApiModels;

/// <summary>
///     API модель категории
/// </summary>
public class CategoryApiModel : CategoryBaseApiModel
{
    /// <summary>
    ///     Идентификатор
    /// </summary>
    public Guid Id { get; set; }
}