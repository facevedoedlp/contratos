namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Entities.Base;

    public class CreateContractObjectiveRequest : Entity<int?>
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsRepeatable { get; set; }
    }
}
