//namespace Zubeldia.Providers.Configuration
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Metadata.Builders;
//    using Zubeldia.Domain.Entities;

//    public class PlayerBirthPlaceConfig : IEntityTypeConfiguration<PlayerBirthPlace>
//    {
//        public void Configure(EntityTypeBuilder<PlayerBirthPlace> builder)
//        {
//            builder.ToTable("PlayerBirthPlaces");
//            builder.HasKey(x => x.Id);

//            builder.Property(x => x.Id).HasColumnOrder(0);
//            builder.Property(x => x.PlayerId).IsRequired().HasColumnOrder(1);
//            builder.Property(x => x.Address).IsRequired().HasMaxLength(200).HasColumnOrder(2);
//            builder.Property(x => x.CityId).IsRequired().HasColumnOrder(3);
//            builder.Property(x => x.StateId).IsRequired().HasColumnOrder(4);
//            builder.Property(x => x.CountryId).IsRequired().HasColumnOrder(5);

//            builder.HasOne(b => b.Player)
//                .WithOne(p => p.BirthPlace)
//                .HasForeignKey<PlayerBirthPlace>(b => b.PlayerId)
//                .OnDelete(DeleteBehavior.Cascade);

//            builder.HasOne(x => x.City)
//                .WithMany()
//                .HasForeignKey(x => x.CityId)
//                .OnDelete(DeleteBehavior.Restrict);

//            builder.HasOne(x => x.State)
//                .WithMany()
//                .HasForeignKey(x => x.StateId)
//                .OnDelete(DeleteBehavior.Restrict);

//            builder.HasOne(x => x.Country)
//                .WithMany()
//                .HasForeignKey(x => x.CountryId)
//                .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}