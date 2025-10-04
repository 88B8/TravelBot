namespace TravelBot.Common.Contracts
{
    /// <summary>
    /// Поставщим даты и времени
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Время сейчас
        /// </summary>
        DateTimeOffset UtcNow();
    }
}
