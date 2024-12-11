namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class ContractTrajectoryConfig : IEntityTypeConfiguration<ContractTrajectory>
    {
        public void Configure(EntityTypeBuilder<ContractTrajectory> builder)
        {
            builder.ToTable("ContractTrajectories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.ContractId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.PaymentDate).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2).HasColumnOrder(3);
            builder.Property(x => x.CurrencyId).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.ExchangeRate).IsRequired().HasPrecision(18, 6).HasColumnOrder(5);

            builder.HasOne(x => x.Contract)
                .WithMany(x => x.Trajectories)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}