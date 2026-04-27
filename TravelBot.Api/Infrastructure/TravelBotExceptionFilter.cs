using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelBot.Api.Exceptions;
using TravelBot.Services.Contracts.Exceptions;

namespace TravelBot.Api.Infrastructure;

/// <summary>
///     Фильтр исключений
/// </summary>
public class TravelBotExceptionFilter : IExceptionFilter
{
    void IExceptionFilter.OnException(ExceptionContext context)
    {
        if (context.Exception is not TravelBotException exception) return;

        switch (exception)
        {
            case TravelBotNotFoundException ex:
                SetDataToContext(new NotFoundObjectResult(new ApiExceptionDetail(ex.Message)), context);
                break;
            case TravelBotInvalidOperationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(ex.Message))
                {
                    StatusCode = StatusCodes.Status406NotAcceptable
                }, context);
                break;
            case TravelBotValidationException ex:
                SetDataToContext(new BadRequestObjectResult(new ApiValidationExceptionDetail
                {
                    Errors = ex.Errors
                })
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                }, context);
                break;
            default:
                SetDataToContext(new BadRequestObjectResult(new ApiExceptionDetail(exception.Message)), context);
                break;
        }
    }

    private static void SetDataToContext(ObjectResult data, ExceptionContext context)
    {
        context.ExceptionHandled = true;
        var response = context.HttpContext.Response;
        response.StatusCode = data.StatusCode ?? StatusCodes.Status400BadRequest;
        context.Result = data;
    }
}