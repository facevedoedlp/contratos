namespace Zubeldia.Dtos.Models.Commons
{
    public class ValidatorResultDTO
    {
        public bool IsValid => Errors.Count == 0;
        public string? EntityNumber { get; set; }
        public int? EntityId { get; set; }
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
