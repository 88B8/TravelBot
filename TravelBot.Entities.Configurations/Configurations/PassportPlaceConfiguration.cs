using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelBot.Entities.Configurations.Configurations;

/// <summary>
///     Модель конфигурации <see cref="PassportPlace" /> для EF Core
/// </summary>
public class PassportPlaceConfiguration : IEntityTypeConfiguration<PassportPlace>, IEntityConfigurationAnchor
{
    public void Configure(EntityTypeBuilder<PassportPlace> builder)
    {
        builder.ToTable("PassportPlaces");

        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.Passport)
            .WithMany(x => x.PassportPlaces)
            .HasForeignKey(x => x.PassportId);

        builder
            .HasOne(x => x.Place)
            .WithMany(x => x.PassportPlaces)
            .HasForeignKey(x => x.PlaceId);
    }
}