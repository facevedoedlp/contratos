namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Entities.Base;

    public class Permission : Entity<int>
    {
        public string Name { get; set; }
        public PermissionResourceTypeEnum Resource { get; set; }
        public PermissionActionEnum Action { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

    }
}
