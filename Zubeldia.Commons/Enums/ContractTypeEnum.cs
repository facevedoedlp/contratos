namespace Zubeldia.Commons.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ContractTypeEnum
    {
        [Display(Name = "AFA")]
        Afa = 1,

        [Display(Name = "Laboral con Objetivos")]
        EmploymentWithObjectives = 2,

        [Display(Name = "Laboral con Objetivos y Trayectoria")]
        EmploymentWithObjectivesAndCareerPath = 3,

        [Display(Name = "Laboral con Objetivos y Trayectoria en Moneda Extranjera")]
        EmploymentWithObjectivesAndCareerPathForeignCurrency = 4,
    }
}
