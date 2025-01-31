namespace Zubeldia.Providers
{
    using System.Data;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;
    using Zubeldia.Domain.Entities;
    using Zubeldia.Domain.Interfaces.Providers;

    public class ZubeldiaDbContext : DbContext, IZubeldiaDbContext
    {
        public ZubeldiaDbContext()
        {
        }

        public ZubeldiaDbContext(DbContextOptions<ZubeldiaDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<Enum>().HaveConversion<short>();
            builder.Properties<decimal>().HavePrecision(19, 5);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public IDbConnection Connection => Database.GetDbConnection();
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractObjective> ContractObjectives { get; set; }
        public virtual DbSet<ContractSalary> ContractSalaries { get; set; }
        public virtual DbSet<ContractTrajectory> ContractTrajectories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<PlayerRelative> PlayerRelatives { get; set; }
        public virtual DbSet<PlayerIdentification> PlayerIdentifications { get; set; }
        public virtual DbSet<PlayerPosition> PlayerPositions { get; set; }
        public virtual DbSet<PlayerAddress> PlayerAddresses { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Discipline> Disciplines { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<HealthcarePlan> HealthcarePlans { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Agent> Agents { get; set; }
    }
}
