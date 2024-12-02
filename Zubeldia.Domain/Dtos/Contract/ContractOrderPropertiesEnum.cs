namespace Zubeldia.Domain.Dtos.Contract
{
    using System.ComponentModel.DataAnnotations;

    public enum ContractOrderPropertiesEnum
    {
        [Display(Name = "Titulo")]
        Title,
        [Display(Name = "Tipo")]
        Type,
        [Display(Name = "Fecha de Inicio")]
        StartDate,
        [Display(Name = "Fecha de Fin")]
        EndDate,
        [Display(Name = "Meses restantes")]
        RemainingMonths,
        [Display(Name = "Id")]
        Id,
    }
}
