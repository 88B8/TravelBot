namespace TravelBot.Services.Contracts.Models.BaseModels
{
    /// <summary>
    /// Базовая модель посещенного места
    /// </summary>
    public abstract class PassportPlaceBaseModel
    {
        /// <summary>
        /// Дата посещения
        /// </summary>
        public DateOnly VisitedAt { get; set; }
    }
}
