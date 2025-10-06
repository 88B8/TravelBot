using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.Models
{
    /// <summary>
    /// Модель пользователя со связанными сущностями
    /// </summary>
    public class UserDbModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Телеграм идентификатор
        /// </summary>
        public long TelegramId { get; set; }

        /// <summary>
        /// Паспорт
        /// </summary>
        public PassportDbModel Passport { get; set; } = null!;
    }
}
