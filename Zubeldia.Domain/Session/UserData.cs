namespace Zubeldia.Domain.Session
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class UserData
    {
        private string userNameValue;
        private string firstNameValue;
        private string lastNameValue;
        private string emailValue;

        public string UserName
        {
            get { return userNameValue; }
        }

        public string FirstName
        {
            get { return firstNameValue; }
        }

        public string LastName
        {
            get { return lastNameValue; }
        }

        public string Email
        {
            get { return emailValue; }
        }

        public void SetUserData(string personIdentifier, string userName, string firstName, string lastName, string email)
        {
            userNameValue = userName;
            firstNameValue = firstName;
            lastNameValue = lastName;
            emailValue = email;
        }
    }
}
