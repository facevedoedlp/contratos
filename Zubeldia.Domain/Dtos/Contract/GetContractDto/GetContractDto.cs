namespace Zubeldia.Domain.Dtos.Contract.GetContractDto
{
    public class GetContractDto
    {
        public string Title { get; set; }
        public string File { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public GetContractPlayerDto Player { get; set; }
        public IEnumerable<GetContractObjectiveDto> Objectives { get; set; }
        public IEnumerable<GetContractSalaryDto> Salaries { get; set; }
    }
}
