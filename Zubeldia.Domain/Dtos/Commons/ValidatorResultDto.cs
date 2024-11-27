namespace Zubeldia.Dtos.Models.Commons
{
    public class ValidatorResultDto
    {
        public bool IsValid => Errors.Count == 0;
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
