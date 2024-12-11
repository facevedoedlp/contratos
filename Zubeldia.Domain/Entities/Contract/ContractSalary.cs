namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;
    public class ContractSalary : Entity<int>
    {
        public int ContractId { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Currency Currency { get; set; }
        public Contract Contract { get; set; }
        public int GetInstallmentsCount() => ((EndDate.Year - StartDate.Year) * 12) + EndDate.Month - StartDate.Month + 1;
        public decimal GetTotalRecognition() => Amount * GetInstallmentsCount();
    }
}