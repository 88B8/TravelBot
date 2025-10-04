namespace TravelBot.Entities.ValidationRules
{
    /// <summary>
    /// Правила валидации для <see cref="Place"/>
    /// </summary>
    public static class PlaceValidationRules
    {
        /// <summary>
        /// Минимальная длина имени пользователя
        /// </summary>
        public const int MinNameLength = 3;

        /// <summary>
        /// Максимальная длина имени пользователя
        /// </summary>
        public const int MaxNameLength = 255;

        /// <summary>
        /// Минимальная длина описания
        /// </summary>
        public const int MinDescriptionLength = 3;

        /// <summary>
        /// Максимальная длина описания
        /// </summary>
        public const int MaxDescriptionLength = 255;

        /// <summary>
        /// Минимальная длина метро
        /// </summary>
        public const int MinMetroLength = 3;

        /// <summary>
        /// Максимальная длина метро
        /// </summary>
        public const int MaxMetroLength = 255;

        /// <summary>
        /// Минимальная длина адреса
        /// </summary>
        public const int MinAddressLength = 3;

        /// <summary>
        /// Максимальная длина адреса
        /// </summary>
        public const int MaxAddressLength = 255;

        /// <summary>
        /// Минимальная длина ссылки
        /// </summary>
        public const int MinLinkLength = 3;

        /// <summary>
        /// Максимальная длина ссылки
        /// </summary>
        public const int MaxLinkLength = 500;
    }
}
