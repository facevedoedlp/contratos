namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum PlayerDoucmentTypeEnum
    {
        [Display(Name = "DNI")]
        IdentityCard = 1,

        [Display(Name = "Pasaporte")]
        Passport = 1,
    }
}
