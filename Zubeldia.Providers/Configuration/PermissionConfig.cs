namespace Zubeldia.Providers.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Zubeldia.Domain.Entities;

    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnOrder(1);
            builder.Property(x => x.Resource).IsRequired().HasColumnOrder(2);
            builder.Property(x => x.Action).IsRequired().HasColumnOrder(3);
        }
    }

}