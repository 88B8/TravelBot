using Microsoft.AspNetCore.CookiePolicy;
using TravelBot.Api.Extensions;

namespace TravelBot.Api;

/// <summary>
///     Точка входа
/// </summary>
public static class Program
{
    private static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTravelBotApi(builder.Configuration, builder.Environment);

        var app = builder.Build();

        app.UseCors(ServiceExtensions.GetCorsPolicyName());

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}