namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;
    public class HealthcarePlan : Entity<int>
    {
        public string Name { get; set; }
    }
}