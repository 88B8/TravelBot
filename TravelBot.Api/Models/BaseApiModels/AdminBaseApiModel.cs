namespace TravelBot.Api.Models.BaseApiModels;

/// <summary>
///     Базовая API-модель администратора
/// </summary>
public abstract class AdminBaseApiModel
{
    /// <summary>
    ///     Имя администратора
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Логин администратора
    /// </summary>
    public string Login { get; set; } = string.Empty;
}