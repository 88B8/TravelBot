using FluentValidation;
using TravelBot.Entities.ValidationRules;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators
{
    /// <summary>
    /// Валидация <see cref="CategoryCreateModel"/>
    /// </summary>
    public class CategoryCreateModelValidator : AbstractValidator<CategoryCreateModel>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CategoryCreateModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя категории обязательно")
                .Length(CategoryValidationRules.MinNameLength, CategoryValidationRules.MaxNameLength)
                .WithMessage($"Длина имени категории должна быть от {CategoryValidationRules.MinNameLength} до {CategoryValidationRules.MaxNameLength}");
        }
    }
}