using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities
{
    /// <summary>
    /// Модель туристического паспорта
    /// </summary>
    public class Passport : BaseAuditEntity
    {
        /// <summary>
        /// Навигационное свойство посещенных мест
        /// </summary>
        public IEnumerable<PassportPlace> PassportPlaces { get; set; } = null!;
    }
}