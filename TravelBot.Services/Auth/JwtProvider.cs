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
        Claim[] claims = [new("userid", admin.Id.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(options.ExpiresHours));

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}