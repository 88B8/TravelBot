namespace TravelBot.Services.Contracts.Exceptions
{
    /// <summary>
    /// Базовый класс исключений
    /// </summary>
    public abstract class TravelBotException : Exception
    {
        /// <summary>
        /// ctor
        /// </summary>
        protected TravelBotException() { }

        /// <summary>
        /// ctor
        /// </summary>
        protected TravelBotException(string message) : base(message) { }
    }
}
