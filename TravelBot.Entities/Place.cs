using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель места
    /// </summary>
    public class Place : BaseAuditEntity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public Guid CategoryId { get; set; }

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

        /// <summary>
        /// Посещенные места
        /// </summary>
        public ICollection<PassportPlace> PassportPlaces { get; set; } = null!;

        /// <summary>
        /// Места
        /// </summary>
        public ICollection<RoutePlace> RoutePlaces { get; set; } = null!;
    }
}
