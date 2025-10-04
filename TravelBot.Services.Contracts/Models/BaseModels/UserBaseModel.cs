using TravelBot.Services.Contracts.Models.Enums;

namespace TravelBot.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель пользователя
    /// </summary>
    public abstract class UserBaseModel
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