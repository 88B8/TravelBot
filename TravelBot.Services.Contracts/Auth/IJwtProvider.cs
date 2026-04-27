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
}