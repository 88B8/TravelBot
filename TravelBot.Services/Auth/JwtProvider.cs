using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Auth;
using TravelBot.Services.Contracts.Models.Auth;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Auth;

/// <inheritdoc cref="IJwtProvider" />
public class JwtProvider : IJwtProvider, IServiceAnchor
{
    private readonly JwtOptions options;

    /// <summary>
    ///     ctor
    /// </summary>
    public JwtProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    string IJwtProvider.GenerateToken(AdminModel admin)
    {
        return GenerateToken(admin.Id, "Admin");
    }

    string IJwtProvider.GenerateToken(UserModel user)
    {
        return GenerateToken(user.Id, "User");
    }

    private string GenerateToken(Guid userId, string role)
    {
        Claim[] claims =
        [
            new("userid", userId.ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, role)
        ];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(options.ExpiresHours));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}