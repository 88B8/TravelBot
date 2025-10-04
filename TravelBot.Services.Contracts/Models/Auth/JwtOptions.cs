namespace TravelBot.Services.Contracts.Models.Auth
{
    /// <summary>
    /// Настройки Jwt
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Издатель
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Аудитория
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Истекает через
        /// </summary>
        public int ExpiresHours { get; set; }
    }
}
