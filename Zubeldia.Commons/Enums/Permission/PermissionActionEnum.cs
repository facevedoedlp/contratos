namespace Zubeldia.Commons.Enums.Permission
{
    using System.ComponentModel.DataAnnotations;

    public enum PermissionActionEnum
    {
        [Display(Name = "Ver")]
        Read = 0,

        [Display(Name = "Crear")]
        Create = 1,

        [Display(Name = "Borrar")]
        Delete = 2,

        [Display(Name = "Editar")]
        Update = 3,
    }
}
