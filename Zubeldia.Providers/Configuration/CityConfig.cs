namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnOrder(1);
            builder.Property(x => x.StateId)
                .IsRequired()
                .HasColumnOrder(2);

            builder.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}