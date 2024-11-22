namespace Zubeldia.Domain.Interfaces.Providers
{
    using System.Data;
    public interface IZubeldiaDbContext
    {
        IDbConnection Connection { get; }
    }
}