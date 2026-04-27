using FluentValidation;
using TravelBot.Api.Client.Client;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Web.Validators;

/// <summary>
///     Валиадтор <see cref="CategoryCreateApiModel" />
/// </summary>
public class CategoryCreateApiModelValidator : AbstractValidator<CategoryCreateApiModel>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public CategoryCreateApiModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя категории обязательно")
            .Length(CategoryValidationRules.MinNameLength, CategoryValidationRules.MaxNameLength)
            .WithMessage(
                $"Длина имени категории должна быть от {CategoryValidationRules.MinNameLength} до {CategoryValidationRules.MaxNameLength} символов");
    }
}