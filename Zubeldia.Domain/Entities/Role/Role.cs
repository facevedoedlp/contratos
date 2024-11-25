namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class Role : Entity<int>
    {
        public string Name { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
    }
}