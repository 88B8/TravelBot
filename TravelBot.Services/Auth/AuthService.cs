using AutoMapper;
using TravelBot.Repositories.Contracts.ReadRepositories;
using TravelBot.Services.Anchors;
using TravelBot.Services.Contracts.Auth;
using TravelBot.Services.Contracts.Exceptions;
using TravelBot.Services.Contracts.Models.Auth;
using TravelBot.Services.Contracts.Models.RequestModels;

namespace TravelBot.Services.Auth;

/// <inheritdoc cref="IAuthService" />
public class AuthService : IAuthService, IServiceAnchor
{
    private readonly IAdminReadRepository adminReadRepository;
    private readonly IJwtProvider jwtProvider;
    private readonly IMapper mapper;
    private readonly IPasswordHasher passwordHasher;

    /// <summary>
    ///     ctor
    /// </summary>
    public AuthService(IAdminReadRepository adminReadRepository, IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider, IMapper mapper)
    {
        this.adminReadRepository = adminReadRepository;
        this.passwordHasher = passwordHasher;
        this.jwtProvider = jwtProvider;
        this.mapper = mapper;
    }

    async Task<string?> IAuthService.Login(LoginRequestModel request, CancellationToken cancellationToken)
    {
        var item = await adminReadRepository.GetByLogin(request.Login, cancellationToken)
                   ?? throw new TravelBotNotFoundException(
                       $"Не удалось найти администратора с логином {request.Login}");

        var result = passwordHasher.Verify(request.Password, item.PasswordHash);

        if (!result) return null;

        var adminModel = mapper.Map<AdminModel>(item);
        var token = jwtProvider.GenerateToken(adminModel);

        return token;
    }
}