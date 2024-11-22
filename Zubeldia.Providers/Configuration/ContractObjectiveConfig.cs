namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class ContractObjectiveConfig : IEntityTypeConfiguration<ContractObjective>
    {
        public void Configure(EntityTypeBuilder<ContractObjective> builder)
        {
            builder.ToTable("ContractObjectives");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.ContractId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150).HasColumnOrder(2);
            builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2).HasColumnOrder(3);
            builder.Property(x => x.CurrencyId).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.Season).HasColumnOrder(5);
            builder.Property(x => x.StartDate).IsRequired().HasColumnOrder(6);
            builder.Property(x => x.EndDate).IsRequired().HasColumnOrder(7);
            builder.Property(x => x.IsCompleted).IsRequired().HasColumnOrder(8);
            builder.Property(x => x.CompletionDate).HasColumnOrder(9);

            builder.HasOne(x => x.Contract)
                .WithMany(x => x.Objectives)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}