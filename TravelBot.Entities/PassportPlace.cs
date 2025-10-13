using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель посещенного места
    /// </summary>
    public class PassportPlace : BaseAuditEntity
    {
        /// <summary>
        /// Идентификатор паспорта
        /// </summary>
        public Guid PassportId { get; set; }

        /// <summary>
        /// Навигационное свойство паспорта
        /// </summary>
        public Passport Passport { get; set; } = null!;

        /// <summary>
        /// Идентификатор места
        /// </summary>
        public Guid PlaceId { get; set; }

        /// <summary>
        /// Навигационное свойство места
        /// </summary>
        public Place Place { get; set; } = null!;
    }
}
