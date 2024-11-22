namespace Zubeldia.Domain.Dtos.Contract
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Commons.Enums;

    public class CreateContractRequest
    {
        public string Title { get; set; }
        public int PlayerId { get; set; }
        public IFormFile File { get; set; }
        public ContractTypeEnum Type { get; set; }
    }
}
