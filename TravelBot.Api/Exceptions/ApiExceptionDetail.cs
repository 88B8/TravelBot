namespace TravelBot.Api.Exceptions;

/// <summary>
///     Информация об ошибке работы API
/// </summary>
public class ApiExceptionDetail
{
    /// <summary>
    ///     ctor
    /// </summary>
    public ApiExceptionDetail(string message)
    {
        Message = message;
    }

    /// <summary>
    ///     Сообщение об ошибке
    /// </summary>
    public string Message { get; set; }
}