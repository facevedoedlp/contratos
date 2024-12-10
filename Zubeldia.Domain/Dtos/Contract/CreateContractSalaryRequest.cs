namespace Zubeldia.Domain.Dtos.Contract
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class CreateContractSalaryRequest : Entity<int?>
    {
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalRecognition { get; set; }
        public int? InstallmentsCount { get; set; }
        public decimal? InstallmentRecognition { get; set; }
    }
}
