using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Contracts.Auth;

/// <summary>
///     Jwt поставщик
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    ///     Генерирует токен
    /// </summary>
    string GenerateToken(AdminModel admin);
    
    /// <summary>
    /// Генерирует токен для пользователя
    /// </summary>
    string GenerateToken(UserModel user);
}