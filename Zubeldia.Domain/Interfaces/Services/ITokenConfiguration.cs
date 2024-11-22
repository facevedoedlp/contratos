namespace Zubeldia.Domain.Interfaces.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface ITokenConfiguration
    {
        void ConfigureAuthentication(IServiceCollection services);
    }
}
