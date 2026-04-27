namespace TravelBot.Entities.ValidationRules;

/// <summary>
///     Правила валидации <see cref="Category" />
/// </summary>
public static class CategoryValidationRules
{
    /// <summary>
    ///     Минимальная длина имени категории
    /// </summary>
    public const int MinNameLength = 2;

    /// <summary>
    ///     Максимальная длина имени категории
    /// </summary>
    public const int MaxNameLength = 100;
}