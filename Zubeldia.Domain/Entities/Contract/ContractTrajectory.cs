namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class ContractTrajectory : Entity<int>
    {
        public int ContractId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public decimal ExchangeRate { get; set; }
        public Contract Contract { get; set; }
        public Currency Currency { get; set; }
    }
}
