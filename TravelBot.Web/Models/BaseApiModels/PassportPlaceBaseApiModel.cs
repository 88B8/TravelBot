namespace TravelBot.Web.Models.BaseApiModels
{
    /// <summary>
    /// Базовая API модель посещенного места
    /// </summary>
    public abstract class PassportPlaceBaseApiModel
    {
        /// <summary>
        /// Дата посещения
        /// </summary>
        public DateOnly VisitedAt { get; set; }
    }
}