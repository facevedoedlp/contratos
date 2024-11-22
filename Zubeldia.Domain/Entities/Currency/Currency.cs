namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;
    public class Currency : Entity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
