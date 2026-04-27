namespace TravelBot.Entities.ValidationRules;

/// <summary>
///     Правила валидации <see cref="Admin" />
/// </summary>
public static class AdminValidationRules
{
    /// <summary>
    ///     Минимальная длина имени администратора
    /// </summary>
    public const int MinNameLength = 2;

    /// <summary>
    ///     Максимальная длина имени администратора
    /// </summary>
    public const int MaxNameLength = 255;

    /// <summary>
    ///     Минимальная длина логина
    /// </summary>
    public const int MinLoginLength = 4;

    /// <summary>
    ///     Максимальная длина логина
    /// </summary>
    public const int MaxLoginLength = 32;

    /// <summary>
    ///     Максимальная длина хэша пароля
    /// </summary>
    public const int MaxPasswordHashLength = 255;

    /// <summary>
    ///     Минимальная длина пароля
    /// </summary>
    public const int MinPasswordLength = 8;

    /// <summary>
    ///     Максимальная длина пароля
    /// </summary>
    public const int MaxPasswordLength = 32;
}