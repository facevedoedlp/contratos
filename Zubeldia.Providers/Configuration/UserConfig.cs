namespace Zubeldia.Providers.Configuration
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(100).HasColumnOrder(1);
            builder.Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(255).HasColumnOrder(2);
            builder.Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(50).HasColumnOrder(3);
            builder.Property(x => x.LastName).HasColumnName("LastName").IsRequired().HasMaxLength(50).HasColumnOrder(4);
        }
    }
}
