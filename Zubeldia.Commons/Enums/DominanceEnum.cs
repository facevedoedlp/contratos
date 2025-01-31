namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum DominanceEnum
    {
        [Display(Name = "Derecho")]
        Right = 0,

        [Display(Name = "Izquierdo")]
        Left = 1,
    }
}
