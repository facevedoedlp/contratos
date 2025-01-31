namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class PlayerIdentification : Entity<int>
    {
        public int PlayerId { get; set; }
        public string Number { get; set; }
        public PlayerDoucmentTypeEnum Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? FrontPhoto { get; set; }
        public string? BackPhoto { get; set; }
        public int CountryId { get; set; }
        public Player Player { get; set; }
        public Country Country { get; set; }
    }
}