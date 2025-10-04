namespace TravelBot.Entities.Contracts
{
    /// <summary>
    /// Сущность с мягким удалением
    /// </summary>
    public interface IEntitySoftDeleted
    {
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
