namespace TravelBot.Services.Contracts.Exceptions;

/// <summary>
///     Модель инвалидации запросов
/// </summary>
public class InvalidateItemModel
{
    /// <summary>
    ///     ctor
    /// </summary>
    public InvalidateItemModel(string field, string message)
    {
        Field = field;
        Message = message;
    }

    /// <summary>
    ///     Имя инвалидного поля
    /// </summary>
    public string Field { get; }

    /// <summary>
    ///     Сообщение инвалидации
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     Создает <see cref="InvalidateItemModel" />
    /// </summary>
    public static InvalidateItemModel New(string field, string message)
    {
        return new InvalidateItemModel(field, message);
    }
}