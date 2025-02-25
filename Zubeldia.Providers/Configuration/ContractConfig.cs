﻿namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class ContractConfig : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.PlayerId).IsRequired().HasColumnOrder(1);
            builder.Property(x => x.File).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.StartDate).IsRequired().HasColumnOrder(3);
            builder.Property(x => x.EndDate).IsRequired().HasColumnOrder(4);
            builder.Property(x => x.EarlyTerminationDate).HasColumnOrder(5);
            builder.Property(x => x.Type).IsRequired().HasColumnOrder(6);
            builder.Property(x => x.ReleaseClause).HasColumnOrder(7);
            builder.Property(x => x.IsAddendum).IsRequired().HasColumnOrder(8);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.Contracts)
                .HasForeignKey(x => x.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Objectives)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Objectives_Contracts_Cascade");

            builder.HasMany(x => x.Salaries)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Salaries_Contracts_Cascade");

            builder.HasMany(x => x.Trajectories)
                .WithOne(x => x.Contract)
                .HasForeignKey(x => x.ContractId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Trajectories_Contracts_Cascade");
        }
    }
}