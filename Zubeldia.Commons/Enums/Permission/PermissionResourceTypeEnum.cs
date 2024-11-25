using System.ComponentModel.DataAnnotations;

namespace Zubeldia.Commons.Enums.Permission
{
    public enum PermissionResourceTypeEnum
    {
        [Display(Name = "Usuarios")]
        Users = 0,

        [Display(Name = "Roles")]
        Roles = 1,

        [Display(Name = "Roles del usuario")]
        UserRoles = 2,
    }
}
