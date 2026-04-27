using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelBot.Services.Contracts.Services.Tools;

namespace TravelBot.Api.Controllers.Tools;

/// <summary>
///     Контроллер генерации QR кодов
/// </summary>
[ApiController]
[Route("api/qr-codes")]
public class QrCodeController : ControllerBase
{
    private readonly IPlaceQrCodeService placeQrCodeService;
    private readonly IQrCodeGenerator qrCodeGenerator;

    /// <summary>
    ///     ctor
    /// </summary>
    public QrCodeController(IQrCodeGenerator qrCodeGenerator, IPlaceQrCodeService placeQrCodeService)
    {
        this.qrCodeGenerator = qrCodeGenerator;
        this.placeQrCodeService = placeQrCodeService;
    }

    /// <summary>
    ///     Сгенерировать QR код
    /// </summary>
    [HttpGet("generate")]
    [SwaggerOperation(OperationId = "QrCodeGenerate")]
    [Produces("image/png")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]    public async Task<IActionResult> Generate([FromQuery] string link, CancellationToken cancellationToken)
    {
        var qrCodeBytes = await qrCodeGenerator.Generate(link, cancellationToken);

        return File(qrCodeBytes, "image/png");
    }

    /// <summary>
    ///     Сгенерировать QR код для добавления места
    /// </summary>
    [HttpGet("places/{placeId:guid}")]
    [Produces("image/png")]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
    [SwaggerOperation(OperationId = "QrCodeGenerateForPlace")]
    public async Task<IActionResult> GenerateForPlace(Guid placeId, CancellationToken cancellationToken)
    {
        var qrCodeBytes = await placeQrCodeService.GenerateForPlace(placeId, cancellationToken);

        return File(qrCodeBytes, "image/png");
    }
}