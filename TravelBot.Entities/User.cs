using TravelBot.Entities.BaseModels;
using TravelBot.Entities.Enums;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class User : BaseAuditEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Телеграм идентификатор
        /// </summary>
        public int TelegramId { get; set; }

        /// <summary>
        /// Идентификатор паспорта
        /// </summary>
        public Guid PassportId { get; set; }
        
        /// <summary>
        /// Навигационное свойство паспорта
        /// </summary>
        public Passport Passport { get; set; } = null!;
    }
}
