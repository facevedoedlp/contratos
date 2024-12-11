namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class Contract : AuditableEntity
    {
        public string File { get; set; }
        public int PlayerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? EarlyTerminationDate { get; set; }
        public ContractTypeEnum Type { get; set; }
        public decimal? ReleaseClause { get; set; }
        public bool IsAddendum { get; set; }
        public Player Player { get; set; }
        public IEnumerable<ContractObjective> Objectives { get; set; }
        public IEnumerable<ContractSalary> Salaries { get; set; }
        public IEnumerable<ContractTrajectory> Trajectories { get; set; }
        public decimal GetTotalTrajectoryAmount() => Trajectories?.Sum(x => x.Amount) ?? 0;
        public decimal GetTrajectoryInstallmentsCount() => Trajectories?.Count() ?? 0;
        public int GetRemainingMonths()
        {
            var today = DateTime.Today;

            if (EndDate < today) return 0;

            var startDate = StartDate > today ? StartDate : today;

            var months = ((EndDate.Year - startDate.Year) * 12) +
                        (EndDate.Month - startDate.Month);

            return EndDate.Day < startDate.Day ? Math.Max(0, months - 1) : Math.Max(0, months);
        }
    }
}
