namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DominanceEnum
    {
        [Display(Name = "Derecho")]
        Right = 1,

        [Display(Name = "Izquierdo")]
        Left = 2,
    }
}
