namespace TravelBot.Services.Contracts.Exceptions;

/// <summary>
///     Исключение NotFound
/// </summary>
public class TravelBotNotFoundException : TravelBotException
{
    /// <summary>
    ///     ctor
    /// </summary>
    public TravelBotNotFoundException(string message) : base(message)
    {
    }
}