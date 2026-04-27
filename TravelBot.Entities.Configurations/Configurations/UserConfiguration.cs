using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Entities.Configurations.Configurations;

/// <summary>
///     Модель конфигурации <see cref="User" /> для EF Core
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>, IEntityConfigurationAnchor
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(UserValidationRules.MaxNameLength)
            .IsRequired();

        builder
            .Property(x => x.TelegramId)
            .IsRequired();
    }
}