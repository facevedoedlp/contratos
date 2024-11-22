namespace Zubeldia.Domain.Session
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SessionData
    {
        private int selectedCompanyValue;
        private UserData userValue;

        public int SelectedCompany
        {
            get { return selectedCompanyValue; }
        }

        public UserData User
        {
            get { return userValue ?? new UserData(); }
        }

        public void SetSessionData(int selectedCompany, UserData user)
        {
            selectedCompanyValue = selectedCompany;
            userValue = user;
        }
    }
}
