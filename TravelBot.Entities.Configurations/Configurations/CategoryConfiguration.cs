using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Entities.Configurations.Configurations;

/// <summary>
///     Модель конфигурации <see cref="Category" /> для EF Core
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>, IEntityConfigurationAnchor
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(CategoryValidationRules.MaxNameLength);
    }
}