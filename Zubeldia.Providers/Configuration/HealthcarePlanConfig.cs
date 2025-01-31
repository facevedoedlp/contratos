namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class HealthcarePlanConfig : IEntityTypeConfiguration<HealthcarePlan>
    {
        public void Configure(EntityTypeBuilder<HealthcarePlan> builder)
        {
            builder.ToTable("HealthcarePlans");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnOrder(1);
        }
    }
}