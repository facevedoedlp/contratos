namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ContractTypeEnum
    {
        [Display(Name = "AFA")]
        Afa = 0,

        [Display(Name = "Laboral con Objetivos")]
        EmploymentWithObjectives = 1,

        [Display(Name = "Laboral con Objetivos y Trayectoria")]
        EmploymentWithObjectivesAndCareerPath = 2,

        [Display(Name = "Laboral con Objetivos y Trayectoria en Moneda Extranjera")]
        EmploymentWithObjectivesAndCareerPathForeignCurrency = 3,
    }
}
