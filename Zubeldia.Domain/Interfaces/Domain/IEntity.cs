namespace Zubeldia.Domain.Interfaces.Domain
{
    internal interface IEntity<T>
    {
        T Id { get; set; }
    }
}
