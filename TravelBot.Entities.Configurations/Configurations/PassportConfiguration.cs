using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelBot.Entities.Configurations.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="Passport"/> для EF Core
    /// </summary>
    public class PassportConfiguration : IEntityTypeConfiguration<Passport>, IEntityConfigurationAnchor
    {
        public void Configure(EntityTypeBuilder<Passport> builder)
        {
            builder.ToTable("Passports");

            builder
                .HasKey(x => x.Id);
        }
    }
}
