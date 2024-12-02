namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Entities.Base;

    public class GetContractsDto : Entity<int>
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RemainingMonths { get; set; }
    }
}
