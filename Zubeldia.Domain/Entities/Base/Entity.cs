namespace Zubeldia.Domain.Entities.Base
{
    using Zubeldia.Domain.Interfaces.Domain;

    public abstract class Entity<T> : IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
