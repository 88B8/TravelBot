using TravelBot.Services.Contracts.Models.BaseModels;

namespace TravelBot.Services.Contracts.Models.CreateModels;

/// <summary>
///     Модель создания администратора
/// </summary>
public class AdminCreateModel : AdminBaseModel
{
    /// <summary>
    ///     Хэш пароля
    /// </summary>
    public string Password { get; set; } = string.Empty;
}