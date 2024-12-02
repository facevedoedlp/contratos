namespace Zubeldia.Commons.Enums.Permission
{
    using System.ComponentModel.DataAnnotations;

    public enum PermissionResourceTypeEnum
    {
        [Display(Name = "Usuarios")]
        Users = 0,

        [Display(Name = "Roles")]
        Roles = 1,

        [Display(Name = "Roles del usuario")]
        UserRoles = 2,

        [Display(Name = "Contratos")]
        Contracts = 3,

        [Display(Name = "Jugadores")]
        Players = 3,
    }
}
