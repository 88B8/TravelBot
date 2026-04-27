using FluentValidation;
using TravelBot.Api.Client.Client;

namespace TravelBot.Web.Validators;

/// <summary>
///     Валидатор <see cref="PassportCreateApiModel" />
/// </summary>
public class PassportCreateApiModelValidator : AbstractValidator<PassportCreateApiModel>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public PassportCreateApiModelValidator()
    {
    }
}