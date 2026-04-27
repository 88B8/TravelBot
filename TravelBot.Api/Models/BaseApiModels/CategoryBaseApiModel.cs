namespace TravelBot.Api.Models.BaseApiModels;

/// <summary>
///     Базовая API модель категории
/// </summary>
public abstract class CategoryBaseApiModel
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; } = string.Empty;
}