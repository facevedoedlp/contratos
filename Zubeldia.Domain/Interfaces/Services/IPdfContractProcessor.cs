namespace Zubeldia.Domain.Interfaces.Services
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Domain.Entities;

    public interface IPdfContractProcessor
    {
        Task<(Contract contract, List<ContractSalary> salaries)> ProcessContractPdf(IFormFile pdfFile, Contract contract);
    }
}
