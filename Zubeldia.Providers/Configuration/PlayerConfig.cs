using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zubeldia.Domain.Entities;

public partial class PlayerConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Players");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnOrder(0);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnOrder(1);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnOrder(2);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100).HasColumnOrder(3);
        builder.Property(x => x.BirthDate).IsRequired().HasColumnOrder(4);
        builder.Property(x => x.IsActive).IsRequired().HasColumnOrder(5);
        builder.Property(x => x.Photo).HasMaxLength(500).HasColumnOrder(6);
        builder.Property(x => x.StartDate).IsRequired().HasColumnOrder(7);
        builder.Property(x => x.DeactivationDate).HasColumnOrder(8);
        builder.Property(x => x.DominanceFoot).IsRequired().HasColumnOrder(9);
        builder.Property(x => x.DominanceEye).IsRequired().HasColumnOrder(10);
        builder.Property(x => x.Gender).IsRequired().HasColumnOrder(11);
        builder.Property(x => x.CountryId).IsRequired().HasColumnOrder(12);
        builder.Property(x => x.StateId).IsRequired().HasColumnOrder(13);
        builder.Property(x => x.CityId).IsRequired().HasColumnOrder(14);
        builder.Property(x => x.PlayerAddressId).HasColumnOrder(15);
        builder.Property(x => x.PlayerHealthcarePlanId).HasColumnOrder(16);
        builder.Property(x => x.DisciplineId).IsRequired().HasColumnOrder(17);
        builder.Property(x => x.CategoryId).IsRequired().HasColumnOrder(18);
        builder.Property(x => x.AgentId).HasColumnOrder(19);
        builder.Property(x => x.CometId).HasColumnOrder(20);
        builder.Property(x => x.FifaId).HasColumnOrder(21);

        builder.HasQueryFilter(x => x.IsActive);

        builder.HasOne(x => x.Nacionality)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.State)
            .WithMany()
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.City)
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Address)
            .WithOne(x => x.Player)
            .HasForeignKey<PlayerAddress>(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.HealthcarePlan)
            .WithOne(x => x.Player)
            .HasForeignKey<PlayerHealthcarePlan>(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Discipline)
            .WithMany()
            .HasForeignKey(x => x.DisciplineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Agent)
            .WithMany()
            .HasForeignKey(x => x.AgentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Relatives)
            .WithOne(x => x.Player)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Identifications)
            .WithOne(x => x.Player)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Positions)
            .WithOne(x => x.Player)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Contracts)
            .WithOne(x => x.Player)
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}