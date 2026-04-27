namespace TravelBot.Services.Contracts.Auth;

/// <summary>
///     Хэшер паролей
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    ///     Генерирует хэш пароля
    /// </summary>
    string Generate(string password);

    /// <summary>
    ///     Сравнивает равенство пароля с хэшем
    /// </summary>
    bool Verify(string password, string passwordHash);
}