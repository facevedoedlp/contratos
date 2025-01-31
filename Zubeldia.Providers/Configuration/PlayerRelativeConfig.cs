namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class PlayerRelativeConfig : IEntityTypeConfiguration<PlayerRelative>
    {
        public void Configure(EntityTypeBuilder<PlayerRelative> builder)
        {
            builder.ToTable("PlayerRelatives");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.PlayerId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasColumnOrder(2);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnOrder(3);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50).HasColumnOrder(4);
            builder.Property(x => x.Relationship).IsRequired().HasColumnOrder(5);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.Relatives)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}