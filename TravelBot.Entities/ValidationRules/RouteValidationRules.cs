namespace TravelBot.Entities.ValidationRules;

/// <summary>
///     Правила валидации <see cref="Route" />
/// </summary>
public static class RouteValidationRules
{
    /// <summary>
    ///     Минимальная длина причины для посещения
    /// </summary>
    public const int MinReasonToVisitLength = 3;

    /// <summary>
    ///     Максимальная длина причины для посещения
    /// </summary>
    public const int MaxReasonToVisitLength = 255;

    /// <summary>
    ///     Минимальная длина отправной точки
    /// </summary>
    public const int MinStartPointLength = 3;

    /// <summary>
    ///     Максимальная длина отправной точки
    /// </summary>
    public const int MaxStartPointLength = 255;

    /// <summary>
    ///     Минимальная длина бюджета
    /// </summary>
    public const int MinBudgetLength = 3;

    /// <summary>
    ///     Максимальная длина бюджета
    /// </summary>
    public const int MaxBudgetLength = 20;
}