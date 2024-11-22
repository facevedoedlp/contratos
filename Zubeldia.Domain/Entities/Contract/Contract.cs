namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class Contract : AuditableEntity
    {
        public string Title { get; set; }
        public byte[] File { get; set; }
        public int PlayerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ContractTypeEnum Type { get; set; }
        public Player Player { get; set; }
        public IEnumerable<ContractObjective> Objectives { get; set; }
        public IEnumerable<ContractSalary> Salaries { get; set; }
    }
}
