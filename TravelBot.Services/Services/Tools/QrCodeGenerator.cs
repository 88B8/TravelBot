using QRCoder;
using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Services.Tools;

namespace TravelBot.Services.Services.Tools;

/// <inheritdoc cref="IQrCodeGenerator" />
public class QrCodeGenerator : IQrCodeGenerator, IServiceAnchor
{
    Task<byte[]> IQrCodeGenerator.Generate(string link, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(link))
            throw new TravelBotInvalidOperationException("Ссылка не может быть пустой");

        using var qrGenerator = new QRCodeGenerator();
        using var qrData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);

        var qrCode = new PngByteQRCode(qrData);
        var qrBytes = qrCode.GetGraphic(20);

        return Task.FromResult(qrBytes);
    }
}