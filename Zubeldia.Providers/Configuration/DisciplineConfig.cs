namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class DisciplineConfig : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("Disciplines");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnOrder(1);

            // Evitar que se eliminen disciplinas que están en uso
            builder.HasMany<Player>()
                .WithOne(x => x.Discipline)
                .HasForeignKey(x => x.DisciplineId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}