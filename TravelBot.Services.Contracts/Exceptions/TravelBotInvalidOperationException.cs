namespace TravelBot.Services.Contracts.Exceptions;

/// <summary>
///     Исключение InvalidOperation
/// </summary>
public class TravelBotInvalidOperationException : TravelBotException
{
    /// <summary>
    ///     ctor
    /// </summary>
    public TravelBotInvalidOperationException(string message) : base(message)
    {
    }
}