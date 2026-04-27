using FluentValidation;
using TravelBot.Api.Client.Client;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Web.Validators;

/// <summary>
///     Валидатор <see cref="PlaceCreateApiModel" />
public class PlaceCreateApiModelValidator : AbstractValidator<PlaceCreateApiModel>
/// </summary>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public PlaceCreateApiModelValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Категория обязательна");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Описание обязательно")
            .Length(PlaceValidationRules.MinDescriptionLength, PlaceValidationRules.MaxDescriptionLength)
            .WithMessage(
                $"Длина описания должна быть от {PlaceValidationRules.MinDescriptionLength} до {PlaceValidationRules.MaxDescriptionLength} символов");

        RuleFor(x => x.Link)
            .NotEmpty()
            .WithMessage("Ссылка обязательна")
            .Length(PlaceValidationRules.MinLinkLength, PlaceValidationRules.MaxLinkLength)
            .WithMessage(
                $"Длина ссылки должна быть от {PlaceValidationRules.MinLinkLength} до {PlaceValidationRules.MaxLinkLength} символов")
            .Must(IsValidLink)
            .WithMessage("Некорректный формат ссылки");

        RuleFor(x => x.Address)
            .NotEmpty()
            .WithMessage("Адрес обязателен")
            .Length(PlaceValidationRules.MinAddressLength, PlaceValidationRules.MaxAddressLength)
            .WithMessage(
                $"Длина адреса должна быть от {PlaceValidationRules.MinAddressLength} до {PlaceValidationRules.MaxAddressLength} символов");

        RuleFor(x => x.Metro)
            .NotEmpty()
            .WithMessage("Метро обязательно")
            .Length(PlaceValidationRules.MinMetroLength, PlaceValidationRules.MaxMetroLength)
            .WithMessage(
                $"Длина описания должна быть от {PlaceValidationRules.MinMetroLength} до {PlaceValidationRules.MaxMetroLength} символов");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя обязательно")
            .Length(PlaceValidationRules.MinNameLength, PlaceValidationRules.MaxNameLength)
            .WithMessage(
                $"Длина описания должна быть от {PlaceValidationRules.MinNameLength} до {PlaceValidationRules.MaxNameLength} символов");
    }

    private static bool IsValidLink(string link)
    {
        return Uri.TryCreate(link, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttps || uriResult.Scheme == Uri.UriSchemeHttp);
    }
}