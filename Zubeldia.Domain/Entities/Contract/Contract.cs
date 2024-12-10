namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class Contract : AuditableEntity
    {
        public string File { get; set; }
        public int PlayerId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ContractTypeEnum Type { get; set; }
        public bool IsAddendum { get; set; }
        public Player Player { get; set; }
        public IEnumerable<ContractObjective> Objectives { get; set; }
        public IEnumerable<ContractSalary> Salaries { get; set; }

        public int? GetRemainingMonths()
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                var today = DateTime.Today;

                if (EndDate < today) return 0;

                var startDate = StartDate > today ? StartDate : today;

                var months = ((EndDate.Value.Year - startDate.Value.Year) * 12) +
                            (EndDate.Value.Month - startDate.Value.Month);

                return EndDate.Value.Day < startDate.Value.Day ? Math.Max(0, months - 1) : Math.Max(0, months);
            }

            return null;
        }
    }
}
