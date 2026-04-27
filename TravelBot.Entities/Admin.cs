using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities;

/// <summary>
///     Модель сущности администратора
/// </summary>
public class Admin : BaseAuditEntity
{
    /// <summary>
    ///     Имя администратора
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Логин администратора
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    ///     Хэш пароля
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
}