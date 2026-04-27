using FluentValidation;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators;

/// <summary>
///     Валидация <see cref="RoutePlaceCreateModel" />
/// </summary>
public class RoutePlaceCreateModelValidator : AbstractValidator<RoutePlaceCreateModel>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public RoutePlaceCreateModelValidator()
    {
        RuleFor(x => x.PlaceId)
            .NotEmpty()
            .WithMessage("Идентификатор места обязателен");
    }
}