using FluentValidation;
using TravelBot.Entities.ValidationRules;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="UserCreateModel"/>
    /// </summary>
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя пользователя обязательно")
                .Length(UserValidationRules.MinNameLength, UserValidationRules.MaxNameLength)
                .WithMessage($"Длина имени пользователя должна быть от {UserValidationRules.MinNameLength} до {UserValidationRules.MaxNameLength} символов");

            RuleFor(x => x.TelegramId)
                .NotNull()
                .WithMessage("Telegram ID обязателен");
        }
    }
}
