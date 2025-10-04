namespace TravelBot.Shared.Models
{
    /// <summary>
    /// Модель запроса логина
    /// </summary>
    public class LoginRequestModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = null!;
    }
}