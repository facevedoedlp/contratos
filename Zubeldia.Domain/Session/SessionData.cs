namespace Zubeldia.Domain.Session
{
    using System.Diagnostics.CodeAnalysis;
    using Zubeldia.Commons.Enums.Permission;

    [ExcludeFromCodeCoverage]
    public class SessionData
    {
        private UserData userValue;
        public UserData User => userValue ?? new UserData();
        public int UserId => User.Id;
        public IEnumerable<PermissionResourceTypeEnum> Resources => User.Permissions.Select(p => p.Resource).Distinct();
        public IEnumerable<PermissionActionEnum> Actions => User.Permissions.Select(p => p.Action).Distinct();

        public void SetSessionData(int selectedCompany, UserData user)
        {
            userValue = user;
        }

        public bool HasPermission(PermissionResourceTypeEnum resource, PermissionActionEnum action)
        {
            return User.Permissions.Any(p => p.Resource == resource && p.Action == action);
        }
    }
}
