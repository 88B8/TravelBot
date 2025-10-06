using System.ComponentModel.DataAnnotations;

namespace TravelBot.Entities.Enums
{
    /// <summary>
    /// Сезон
    /// </summary>
    public enum Season
    {
        /// <summary>
        /// Круглый год
        /// </summary>
        [Display(Name = "Круглый год")]
        AllYear = 0,

        /// <summary>
        /// Зима
        /// </summary>
        [Display(Name = "Зима")]
        Winter = 1,

        /// <summary>
        /// Лето
        /// </summary>
        [Display(Name = "Лето")]
        Summer = 2,

        /// <summary>
        /// Лето-осень
        /// </summary>
        [Display(Name = "Лето-осень")]
        SummerAutumn = 3,

        /// <summary>
        /// Весна-лето
        /// </summary>
        [Display(Name = "Весна-лето")]
        SpringSummer = 4,
    }
}
