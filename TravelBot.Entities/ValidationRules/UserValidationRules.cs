namespace TravelBot.Entities.ValidationRules;

/// <summary>
///     Правила валидации для <see cref="User" />
/// </summary>
public static class UserValidationRules
{
    /// <summary>
    ///     Минимальная длина имени пользователя
    /// </summary>
    public const int MinNameLength = 2;

    /// <summary>
    ///     Максимальная длина имени пользователя
    /// </summary>
    public const int MaxNameLength = 255;
}