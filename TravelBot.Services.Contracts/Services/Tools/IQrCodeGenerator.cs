namespace TravelBot.Services.Contracts.Services.Tools;

/// <summary>
///     Генератор QR кодов
/// </summary>
public interface IQrCodeGenerator
{
    /// <summary>
    ///     Сгенерировать QR код
    /// </summary>
    Task<byte[]> Generate(string link, CancellationToken cancellationToken);
}