using TravelBot.Web.Models.Enums;

namespace TravelBot.Web.Models.BaseApiModels
{
    /// <summary>
    /// Базовая API модель пользователя
    /// </summary>
    public abstract class UserBaseApiModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Телеграм идентификатор
        /// </summary>
        public int TelegramId { get; set; }
    }
}