namespace TravelBot.Services.Contracts.Models.BaseModels;

/// <summary>
///     Базовая модель администратора
/// </summary>
public abstract class AdminBaseModel
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