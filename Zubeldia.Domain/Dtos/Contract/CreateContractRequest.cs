namespace Zubeldia.Domain.Dtos.Contract
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class CreateContractRequest : Entity<int?>
    {
        public int PlayerId { get; set; }
        public IFormFile? File { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? EarlyTerminationDate { get; set; }
        public ContractTypeEnum Type { get; set; }
        public decimal? ReleaseClause { get; set; }
        public bool IsAddendum { get; set; }
        public IEnumerable<CreateContractObjectiveRequest> Objectives { get; set; } = new List<CreateContractObjectiveRequest>();
        public IEnumerable<CreateContractSalaryRequest> Salaries { get; set; } = new List<CreateContractSalaryRequest>();
        public IEnumerable<CreateContractTrajectoryRequest> Trajectories { get; set; } = new List<CreateContractTrajectoryRequest>();
    }
}
