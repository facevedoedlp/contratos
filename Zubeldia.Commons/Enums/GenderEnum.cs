namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum GenderEnum
    {
        [Display(Name = "Masculino")]
        Male = 0,

        [Display(Name = "Femenino")]
        Female = 1,
    }
}
