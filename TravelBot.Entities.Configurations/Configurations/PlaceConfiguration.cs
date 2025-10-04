using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Entities.Configurations.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="Place"/> для EF Core
    /// </summary>
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>, IEntityConfigurationAnchor
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.ToTable("Places");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(PlaceValidationRules.MaxNameLength)
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasMaxLength(PlaceValidationRules.MaxDescriptionLength)
                .IsRequired();

            builder
                .Property(x => x.ChildFriendly)
                .IsRequired();

            builder
                .Property(x => x.Metro)
                .HasMaxLength(PlaceValidationRules.MaxMetroLength)
                .IsRequired();

            builder
                .Property(x => x.Address)
                .HasMaxLength(PlaceValidationRules.MaxAddressLength)
                .IsRequired();

            builder
                .Property(x => x.Link)
                .HasMaxLength(PlaceValidationRules.MaxLinkLength)
                .IsRequired();

            builder
                .HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId);
        }
    }
}