namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Entities.Base;

    public class CreateContractTrajectoryRequest : Entity<int?>
    {
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public string? CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
