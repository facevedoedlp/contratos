namespace Zubeldia.Services.Session
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Session;

    public class SessionAccessor(IHttpContextAccessor context)
        : ISessionAccessor
    {
        public SessionData Data => ((SessionData)context?.HttpContext?.Items["sessionData"]) ?? new SessionData();
    }
}
