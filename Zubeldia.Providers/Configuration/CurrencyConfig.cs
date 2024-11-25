namespace Zubeldia.Providers.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.Code).HasColumnType("varchar").IsRequired().HasMaxLength(3).HasColumnOrder(2);
        }
    }
}
