namespace TravelBot.Web.Models.BaseApiModels
{
    /// <summary>
    /// Базовая API модель места
    /// </summary>
    public abstract class PlaceBaseApiModel
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