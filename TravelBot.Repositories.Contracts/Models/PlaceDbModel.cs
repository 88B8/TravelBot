using TravelBot.Entities;

namespace TravelBot.Repositories.Contracts.Models
{
    /// <summary>
    /// Модель места с заполненными связанными сущностями
    /// </summary>
    public class PlaceDbModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Категория
        /// </summary>
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Можно ли с детьми
        /// </summary>
        public bool ChildFriendly { get; set; }

        /// <summary>
        /// Метро
        /// </summary>
        public string Metro { get; set; } = string.Empty;

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Ссылка на место
        /// </summary>
        public string Link { get; set; } = string.Empty;
    }
}
