namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class ContractSalaryConfig : IEntityTypeConfiguration<ContractSalary>
    {
        public void Configure(EntityTypeBuilder<ContractSalary> builder)
        {
            builder.ToTable("ContractSalaries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.ContractId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2).HasColumnOrder(2);
            builder.Property(x => x.CurrencyId).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.ExchangeRate).IsRequired().HasPrecision(18, 6).HasColumnOrder(4);
            builder.Property(x => x.StartDate).IsRequired().HasColumnOrder(5);
            builder.Property(x => x.EndDate).IsRequired().HasColumnOrder(6);
            builder.Property(x => x.TotalRecognition).HasPrecision(18, 2).HasColumnOrder(7);
            builder.Property(x => x.InstallmentsCount).HasColumnOrder(8);
            builder.Property(x => x.InstallmentRecognition).HasPrecision(18, 2).HasColumnOrder(9);

            builder.HasOne(x => x.Contract)
                .WithMany(x => x.Salaries)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Currency)
                .WithMany()
                .HasForeignKey(x => x.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}