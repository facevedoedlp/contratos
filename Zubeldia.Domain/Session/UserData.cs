namespace Zubeldia.Domain.Session
{
    using System.Diagnostics.CodeAnalysis;
    using Zubeldia.Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class UserData
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string FullName => $"{Name} {LastName}";
        public List<Permission> Permissions { get; private set; } = [];

        public void SetUserData(
            int id,
            string userName,
            string name,
            string lastName,
            string email,
            List<Permission> permissions)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            Permissions = permissions;
        }
    }
}