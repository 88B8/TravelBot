namespace TravelBot.Bot.Contracts.Services;

/// <summary>
/// Сервис работы с туристическим паспортом
/// </summary>
public interface IPassportService
{
    /// <summary>
    /// Отобразить паспорт
    /// </summary>
    Task ShowPassport(long telegramId, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить место в паспорт
    /// </summary>
    Task AddPlaceToPassport(long telegramId, Guid placeId, CancellationToken cancellationToken);
}