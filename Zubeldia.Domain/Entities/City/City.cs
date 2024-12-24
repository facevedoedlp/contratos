namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;
    public class City : Entity<int>
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
    }
}