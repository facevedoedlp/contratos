using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zubeldia.Domain.Entities;

public partial class PlayerConfig
{
    public class PlayerAddressConfig : IEntityTypeConfiguration<PlayerAddress>
    {
        public void Configure(EntityTypeBuilder<PlayerAddress> builder)
        {
            builder.ToTable("PlayerAddresses");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.PlayerId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.CountryId).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.ProvinceId).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.CityId).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(100).HasColumnOrder(5);
            builder.Property(x => x.Number).IsRequired().HasMaxLength(20).HasColumnOrder(6);
            builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(10).HasColumnOrder(7);

            builder.HasOne(x => x.Player)
                .WithOne(x => x.Address)
                .HasForeignKey<PlayerAddress>(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}