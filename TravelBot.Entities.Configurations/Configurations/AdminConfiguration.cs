using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Entities.Configurations.Configurations
{
    /// <summary>
    /// Конфигурация <see cref="Admin"/> для EF Core
    /// </summary>
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder
                .ToTable("Admins");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .HasMaxLength(AdminValidationRules.MaxNameLength);

            builder
                .Property(x => x.Login)
                .HasMaxLength(AdminValidationRules.MaxLoginLength);

            builder
                .Property(x => x.PasswordHash)
                .HasMaxLength(AdminValidationRules.MaxPasswordHashLength);
        }
    }
}
