namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class PlayerBirthPlace : Entity<int>
    {
        public int PlayerId { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public Player Player { get; set; }
        public City City { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
    }
}