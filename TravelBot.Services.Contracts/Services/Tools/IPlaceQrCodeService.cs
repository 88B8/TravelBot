namespace TravelBot.Services.Contracts.Services.Tools;

/// <summary>
///     Сервис генерации QR-кодов для места
/// </summary>
public interface IPlaceQrCodeService
{
    /// <summary>
    ///     Сгенерировать
    /// </summary>
    Task<byte[]> GenerateForPlace(Guid placeId, CancellationToken cancellationToken);
}