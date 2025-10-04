using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelBot.Entities.Configurations.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="RoutePlace"/> для EF Core
    /// </summary>
    public class RoutePlaceConfiguration : IEntityTypeConfiguration<RoutePlace>, IEntityConfigurationAnchor
    {
        public void Configure(EntityTypeBuilder<RoutePlace> builder)
        {
            builder.ToTable("RoutePlaces");

            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Place)
                .WithMany(x => x.RoutePlaces)
                .HasForeignKey(x => x.PlaceId);

            builder
                .HasOne(x => x.Route)
                .WithMany(x => x.RoutePlaces)
                .HasForeignKey(x => x.RouteId);
        }
    }
}
