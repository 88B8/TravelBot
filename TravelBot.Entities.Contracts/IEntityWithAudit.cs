namespace TravelBot.Entities.Contracts
{
    /// <summary>
    /// Сущность с полями аудита
    /// </summary>
    public interface IEntityWithAudit
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
