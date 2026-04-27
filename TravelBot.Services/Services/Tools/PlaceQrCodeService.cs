using TravelBot.Constants;
using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Services.Tools;

namespace TravelBot.Services.Services.Tools;

/// <inheritdoc cref="IPlaceQrCodeService" />
public class PlaceQrCodeService : IPlaceQrCodeService, IServiceAnchor
{
    private readonly IQrCodeGenerator qrCodeGenerator;

    /// <summary>
    ///     ctor
    /// </summary>
    public PlaceQrCodeService(IQrCodeGenerator qrCodeGenerator)
    {
        this.qrCodeGenerator = qrCodeGenerator;
    }

    public Task<byte[]> GenerateForPlace(Guid placeId, CancellationToken cancellationToken)
    {
        var link = $"https://t.me/{BotConstants.BotUsername}?start={placeId}";

        return qrCodeGenerator.Generate(link, cancellationToken);
    }
}