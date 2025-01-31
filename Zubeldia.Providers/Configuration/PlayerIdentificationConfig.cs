using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zubeldia.Domain.Entities;

public class PlayerIdentificationConfig : IEntityTypeConfiguration<PlayerIdentification>
{
    public void Configure(EntityTypeBuilder<PlayerIdentification> builder)
    {
        builder.ToTable("PlayerIdentifications");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnOrder(0);
        builder.Property(x => x.PlayerId).IsRequired().HasColumnOrder(1);
        builder.Property(x => x.Number).IsRequired().HasMaxLength(50).HasColumnOrder(2);
        builder.Property(x => x.Type).IsRequired().HasColumnOrder(3);
        builder.Property(x => x.ExpirationDate).IsRequired().HasColumnOrder(4);
        builder.Property(x => x.CountryId).IsRequired().HasColumnOrder(5);
        builder.Property(x => x.FrontPhoto).HasMaxLength(500).HasColumnOrder(6);
        builder.Property(x => x.BackPhoto).HasMaxLength(500).HasColumnOrder(7);

        builder.HasOne(x => x.Player)
            .WithMany(x => x.Identifications)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}