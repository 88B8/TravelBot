using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TravelBot.Common
{
    /// <summary>
    /// Расширения для enum
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Получить отображаемое имя
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());

            if (memberInfo.Length == 0)
            {
                return enumValue.ToString();
            }

            var attribute = memberInfo[0]
                .GetCustomAttribute<DisplayAttribute>();

            return attribute?.Name ?? enumValue.ToString();
        }
    }
}
