using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
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
    private const string CorsPolicyName = "Policy";

    /// <summary>
    ///     Регистрирует все зависимости API
    /// </summary>
    public static IServiceCollection AddTravelBotApi(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        serviceCollection.AddTravelBotCors();
        serviceCollection.AddTravelBotDbContext(configuration);
        serviceCollection.RegisterDependencies();
        serviceCollection.AddApiAuthentication(configuration);
        serviceCollection.AddTravelBotControllers(environment);
        serviceCollection.AddTravelBotOptions(configuration);
        serviceCollection.AddTravelBotSwagger();

        return serviceCollection;
    }

    /// <summary>
    ///     Регистрирует зависимости
    /// </summary>
    public static IServiceCollection RegisterDependencies(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        serviceCollection.RegisterServices();
        serviceCollection.RegisterRepositories();
        serviceCollection.RegisterContext();
        serviceCollection.AddAutoMapper(typeof(ServiceProfile), typeof(ApiMapper));

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет CORS
    /// </summary>
    public static IServiceCollection AddTravelBotCors(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName, config =>
            {
                config.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет DbContext
    /// </summary>
    public static IServiceCollection AddTravelBotDbContext(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string DefaultConnection не найден.");

        serviceCollection.AddDbContext<TravelBotContext>(options =>
            options.UseNpgsql(connectionString)
                .LogTo(Console.WriteLine));

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет контроллеры
    /// </summary>
    public static IServiceCollection AddTravelBotControllers(
        this IServiceCollection serviceCollection,
        IWebHostEnvironment environment)
    {
        var controllers = serviceCollection.AddControllers(options =>
        {
            options.Filters.Add<TravelBotExceptionFilter>();
        });

        if (environment.EnvironmentName == EnvironmentNames.Integration)
            controllers.AddControllersAsServices();

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет Swagger
    /// </summary>
    public static IServiceCollection AddTravelBotSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEndpointsApiExplorer();

        serviceCollection.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            var baseDirectory = AppContext.BaseDirectory;

            c.IncludeXmlComments(Path.Combine(baseDirectory, "TravelBot.Api.xml"));
            c.IncludeXmlComments(Path.Combine(baseDirectory, "TravelBot.Entities.xml"));
        });

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет настройки
    /// </summary>
    public static IServiceCollection AddTravelBotOptions(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.Configure<JwtOptions>(
            configuration.GetSection(nameof(JwtOptions)));

        return serviceCollection;
    }

    /// <summary>
    ///     Добавляет API аутентификацию
    /// </summary>
    public static IServiceCollection AddApiAuthentication(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

        if (jwtOptions == null)
            throw new InvalidOperationException("JwtOptions не настроены в конфигурации.");

        if (string.IsNullOrWhiteSpace(jwtOptions.SecretKey))
            throw new InvalidOperationException("JwtOptions:SecretKey не настроен.");

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

        return serviceCollection;
    }

    /// <summary>
    /// 
    /// </summary>
    public static string GetCorsPolicyName() => CorsPolicyName;
}