using System.Text.RegularExpressions;
using FluentValidation;
using TravelBot.Entities.ValidationRules;
using TravelBot.Services.Contracts.Models.CreateModels;

namespace TravelBot.Services.Validators;

/// <summary>
///     Валидация <see cref="RouteCreateModel" />
/// </summary>
public class RouteCreateModelValidator : AbstractValidator<RouteCreateModel>
{
    /// <summary>
    ///     ctor
    /// </summary>
    public RouteCreateModelValidator()
    {
        RuleFor(x => x.Budget)
            .NotEmpty()
            .WithMessage("Бюджет обязателен")
            .Length(RouteValidationRules.MinBudgetLength, RouteValidationRules.MaxBudgetLength)
            .WithMessage(
                $"Длина бюджета должна быть от {RouteValidationRules.MinBudgetLength} до {RouteValidationRules.MaxBudgetLength} символов")
            .Must(IsValidBudget)
            .WithMessage("Некорректный формат бюджета");

        RuleFor(x => x.ReasonToVisit)
            .NotEmpty()
            .WithMessage("Причина для посещения обязательна")
            .Length(RouteValidationRules.MinReasonToVisitLength, RouteValidationRules.MaxReasonToVisitLength)
            .WithMessage(
                $"Длина причины для посещения должна быть от {RouteValidationRules.MinReasonToVisitLength} до {RouteValidationRules.MaxReasonToVisitLength} символов");

        RuleFor(x => x.StartPoint)
            .NotEmpty()
            .WithMessage("Отправная точка обязательна")
            .Length(RouteValidationRules.MinStartPointLength, RouteValidationRules.MaxStartPointLength)
            .WithMessage(
                $"Длина отправной точки должна быть от {RouteValidationRules.MinStartPointLength} до {RouteValidationRules.MaxStartPointLength} символов");
    }

    private static bool IsValidBudget(string budget)
    {
        var match = Regex.Match(budget, @"^(\d+)-(\d+)$");
        if (!match.Success)
            return false;

        var parts = budget.Trim().Split('-');
        if (int.TryParse(parts[0], out var minValue)
            && int.TryParse(parts[1], out var maxValue))
            return minValue >= 0 && minValue < maxValue;

        return false;
    }
}