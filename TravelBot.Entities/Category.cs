using TravelBot.Entities.BaseModels;

namespace TravelBot.Entities
{
    /// <summary>
    /// Категории мест
    /// </summary>
    public class Category : BaseSoftDeletedEntity
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
