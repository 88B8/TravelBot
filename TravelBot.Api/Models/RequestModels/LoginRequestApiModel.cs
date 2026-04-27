namespace TravelBot.Api.Models.RequestModels;

/// <summary>
/// API модель запроса авторизации
/// </summary>
public class LoginRequestApiModel
{
    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = string.Empty;
}