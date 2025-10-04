using FluentValidation;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="PassportPlaceCreateModel"/>
    /// </summary>
    public class PassportPlaceCreateModelValidator : AbstractValidator<PassportPlaceCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PassportPlaceCreateModelValidator()
        {
            RuleFor(x => x.PlaceId)
                .NotEmpty()
                .WithMessage("Идентификатор места обязателен");
        }
    }
}
