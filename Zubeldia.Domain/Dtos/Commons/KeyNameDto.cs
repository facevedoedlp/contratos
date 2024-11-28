namespace Zubeldia.Domain.Dtos.Commons
{
    public class KeyNameDTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
