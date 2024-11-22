namespace Zubeldia.Domain.Interfaces.Providers
{
    using Zubeldia.Domain.Session;

    public interface ISessionAccessor
    {
        SessionData? Data { get; }
    }
}
