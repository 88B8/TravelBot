using FluentValidation;
using TravelBot.Entities.ValidationRules;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="AdminCreateModel"/> 
    /// </summary>
    public class AdminCreateModelValidator : AbstractValidator<AdminCreateModel>
    {
        public AdminCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя обязательно")
                .Length(AdminValidationRules.MinNameLength, AdminValidationRules.MaxNameLength)
                .WithMessage($"Длина имени должна быть от {AdminValidationRules.MinNameLength} до {AdminValidationRules.MaxNameLength} символов");


            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Логин обязателен")
                .Length(AdminValidationRules.MinLoginLength, AdminValidationRules.MaxLoginLength)
                .WithMessage($"Длина логина должна быть от {AdminValidationRules.MinLoginLength} до {AdminValidationRules.MaxLoginLength} символов");


            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль обязателен")
                .Length(AdminValidationRules.MinNameLength, AdminValidationRules.MaxNameLength)
                .WithMessage($"Длина пароля должна быть от {AdminValidationRules.MinNameLength} до {AdminValidationRules.MaxNameLength} символов");
        }
    }
}
