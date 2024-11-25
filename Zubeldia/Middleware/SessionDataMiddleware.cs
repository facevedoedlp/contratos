namespace Zubeldia.Middleware
{
    using System.Security.Claims;
    using Zubeldia.Domain.Interfaces.Services;
    using Zubeldia.Domain.Session;

    public class SessionDataMiddleware
    {
        private readonly RequestDelegate next;

        public SessionDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAuthenticateService userService)
        {
            var user = httpContext?.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
                var permissions = await userService.GetUserPermissions(userId);

                var userData = new UserData();
                userData.SetUserData(
                    userId,
                    user.FindFirstValue(ClaimTypes.Name),
                    user.FindFirstValue(ClaimTypes.GivenName),
                    user.FindFirstValue(ClaimTypes.Surname),
                    user.FindFirstValue(ClaimTypes.Email),
                    permissions.ToList()
                );

                var sessionData = new SessionData();
                sessionData.SetSessionData(userId, userData);
                httpContext.Items["SessionData"] = sessionData;
            }

            await next(httpContext);
        }
    }
}