using TravelBot.Services.Contracts.Auth;

namespace TravelBot.Services.Auth
{
    /// <inheritdoc cref="IPasswordHasher"/>
    public class PasswordHasher : IPasswordHasher, IServiceAnchor
    {
        string IPasswordHasher.Generate(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        bool IPasswordHasher.Verify(string password, string passwordHash)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}