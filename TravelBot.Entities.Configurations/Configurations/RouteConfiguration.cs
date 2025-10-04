using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelBot.Entities.ValidationRules;

namespace TravelBot.Entities.Configurations.Configurations
{
    /// <summary>
    /// Модель конфигурации <see cref="Route"/> для EF Core
    /// </summary>
    public class RouteConfiguration : IEntityTypeConfiguration<Route>, IEntityConfigurationAnchor
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.ToTable("Routes");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.ReasonToVisit)
                .HasMaxLength(RouteValidationRules.MaxReasonToVisitLength)
                .IsRequired();

            builder
                .Property(x => x.StartPoint)
                .HasMaxLength(RouteValidationRules.MaxStartPointLength)
                .IsRequired();

            builder
                .Property(x => x.Season)
                .IsRequired();

            builder
                .Property(x => x.Budget)
                .HasMaxLength(RouteValidationRules.MaxBudgetLength)
                .IsRequired();

            builder
                .Property(x => x.AverageTime)
                .IsRequired();
        }
    }
}