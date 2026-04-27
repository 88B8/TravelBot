using FluentValidation;
using TravelBot.Api.Client.Client;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Web.Validators;

/// <summary>
///     Валидатор <see cref="UserCreateApiModel" />
/// </summary>
public class UserCreateApiModelValidator : AbstractValidator<UserCreateApiModel>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public UserCreateApiModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Имя пользователя обязательно")
            .Length(UserValidationRules.MinNameLength, UserValidationRules.MaxNameLength)
            .WithMessage(
                $"Длина имени пользователя должна быть от {UserValidationRules.MinNameLength} до {UserValidationRules.MaxNameLength} символов");

        RuleFor(x => x.TelegramId)
            .NotNull()
            .WithMessage("Telegram ID обязателен");
    }
}