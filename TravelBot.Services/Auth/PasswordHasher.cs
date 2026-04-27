using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Auth;

namespace TravelBot.Services.Auth;

/// <inheritdoc cref="IPasswordHasher" />
public class PasswordHasher : IPasswordHasher, IServiceAnchor
{
    string IPasswordHasher.Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    bool IPasswordHasher.Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}