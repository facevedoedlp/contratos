namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Entities.Base;

    public class CreateContractSalaryRequest : Entity<int?>
    {
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
