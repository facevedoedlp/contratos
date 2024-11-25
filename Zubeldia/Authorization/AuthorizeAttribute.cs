namespace Zubeldia.Authorization
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Zubeldia.Commons.Enums.Permission;
    using Zubeldia.Domain.Session;

    public class AuthorizeAttribute : ActionFilterAttribute
    {
        private readonly PermissionResourceTypeEnum resource;
        private readonly PermissionActionEnum action;

        public AuthorizeAttribute(PermissionResourceTypeEnum resource, PermissionActionEnum action)
        {
            this.resource = resource;
            this.action = action;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sessionData = context.HttpContext.Items["SessionData"] as SessionData;

            if (sessionData == null || sessionData.UserId == 0)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!sessionData.HasPermission(resource, action))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            await next();
        }
    }
}