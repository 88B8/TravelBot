using TravelBot.Services.Contracts.Exceptions;

namespace TravelBot.Api.Exceptions;

/// <summary>
///     Информация об ошибках валидации работы API
/// </summary>
public class ApiValidationExceptionDetail
{
    /// <summary>
    ///     Ошибки валидации
    /// </summary>
    public IEnumerable<InvalidateItemModel> Errors { get; set; } = [];
}