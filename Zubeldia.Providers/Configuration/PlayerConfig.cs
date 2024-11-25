namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities.Player;

    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30).HasColumnOrder(1);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50).HasColumnOrder(2);
            builder.Property(x => x.DocumentNumber).IsRequired().HasColumnOrder(3);
        }
    }
}