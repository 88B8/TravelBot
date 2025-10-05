using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using TravelBot.Context;
using TravelBot.Services.Contracts.Models.Auth;
using TravelBot.Web.Extensions;
using TravelBot.Web.Infrastructure;

namespace TravelBot.Web
{
    /// <summary>
    /// Точка входа
    /// </summary>
    public class Program
    {
        private static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Policy", config =>
                {
                    config.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddDbContext<TravelBotContext>(options =>
                options.UseNpgsql(connectionString)
                    .LogTo(Console.WriteLine));

            builder.Services.RegisterDependencies();
            builder.Services.AddApiAuthentication(builder.Configuration);
            var controllers = builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<TravelBotExceptionFilter>();
            });

            if (builder.Environment.EnvironmentName == EnvironmentNames.Integration)
            {
                controllers.AddControllersAsServices();
            }

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var baseDirectory = AppContext.BaseDirectory;
                c.IncludeXmlComments(Path.Combine(baseDirectory, "TravelBot.Web.xml"));
                c.IncludeXmlComments(Path.Combine(baseDirectory, "TravelBot.Entities.xml"));
            });

            var app = builder.Build();

            app.UseCors("Policy");

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
                Secure = CookieSecurePolicy.Always,
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}