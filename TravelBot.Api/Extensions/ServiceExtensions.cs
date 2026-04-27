using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TravelBot.Api.Infrastructure;
using TravelBot.Common.Contracts;
using TravelBot.Common.Infrastructure;
using TravelBot.Context;
using TravelBot.Repositories.Extensions;
using TravelBot.Services.Contracts.Models.Auth;
using TravelBot.Services.Extensions;
using TravelBot.Services.Infrastructure;

namespace TravelBot.Api.Extensions;

/// <summary>
///     Расширения для <see cref="IServiceCollection" />
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    ///     Регистрирует зависимости
    /// </summary>
    public static void RegisterDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        serviceCollection.RegisterServices();
        serviceCollection.RegisterRepositories();
        serviceCollection.RegisterContext();
        serviceCollection.AddAutoMapper(typeof(ServiceProfile), typeof(ApiMapper));
    }

    /// <summary>
    ///     Добавляет API аутентификацию
    /// </summary>
    public static void AddApiAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
        if (jwtOptions == null)
            throw new InvalidOperationException("JwtOptions не настроены в конфигурации.");

        serviceCollection.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        serviceCollection.AddAuthorization();
    }
}