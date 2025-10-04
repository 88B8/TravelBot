namespace TravelBot.Services.Contracts.Services
{
    /// <summary>
    /// Сервис валидации
    /// </summary>
    public interface IValidateService
    {
        /// <summary>
        /// Валидирует модель
        /// </summary>
        Task Validate<TModel>(TModel model, CancellationToken cancellationToken)
            where TModel : class;
    }
}