namespace Zubeldia.Domain.Dtos.Player
{
    using Zubeldia.Domain.Entities.Base;

    public class GetPlayersResponse : Entity<int>
    {
        public string FullName { get; set; }
        public string IdentificationNumber { get; set; }
        public string Discipline { get; set; }
        public string Category { get; set; }
        public string Nacionality { get; set; }
        public string Positions { get; set; }
        public bool IsActive { get; set; }
    }
}
