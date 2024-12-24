namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class Country : Entity<int>
    {
        public string Name { get; set; }
    }
}