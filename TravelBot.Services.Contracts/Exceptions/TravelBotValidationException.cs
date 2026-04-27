namespace TravelBot.Services.Contracts.Exceptions;

/// <summary>
///     Исключение валидации
/// </summary>
public class TravelBotValidationException : TravelBotException
{
    /// <summary>
    ///     ctor
    /// </summary>
    public TravelBotValidationException(IEnumerable<InvalidateItemModel> errors)
    {
        Errors = errors;
    }

    /// <summary>
    ///     Ошибки
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; }
}