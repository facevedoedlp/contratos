namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class Agent : Entity<int>
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public bool IsAgency { get; set; }
    }
}
