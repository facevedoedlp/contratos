namespace Zubeldia.Domain.Dtos.Authentication
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
