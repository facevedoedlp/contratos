namespace Zubeldia.Domain.Dtos.Authentication
{
    public class LoginResponse
    {
        public string Access { get; set; }
        public UserDto User { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
