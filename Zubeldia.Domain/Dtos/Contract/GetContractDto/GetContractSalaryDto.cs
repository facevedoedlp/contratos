﻿namespace Zubeldia.Domain.Dtos.Contract.GetContractDto
{
    public class GetContractSalaryDto
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
