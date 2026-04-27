using TravelBot.Api.Models.BaseApiModels;

namespace TravelBot.Api.Models.CreateApiModels;

/// <summary>
///     API модель создания администратора
/// </summary>
public class AdminCreateApiModel : AdminBaseApiModel
{
    /// <summary>
    ///     Пароль администратора
    /// </summary>
    public string Password { get; set; } = string.Empty;
}