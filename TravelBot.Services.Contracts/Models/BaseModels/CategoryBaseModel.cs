namespace TravelBot.Services.Contracts.Models.BaseModels;

/// <summary>
///     Базовая модель категории
/// </summary>
public abstract class CategoryBaseModel
{
    /// <summary>
    ///     Название категории
    /// </summary>
    public string Name { get; set; } = string.Empty;
}