namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class ContractObjective : Entity<int>
    {
        public int ContractId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsRepeatable { get; set; }
        public int TimesAchieved { get; set; }
        public Contract Contract { get; set; }
        public Currency Currency { get; set; }

        public bool IsCompleted() => TimesAchieved > 0;
    }
}
