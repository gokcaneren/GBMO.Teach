namespace GBMO.Teach.Core.DTOs.Output.Auth.User
{
    public class UserLoginOutput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime LoginDate { get; set; } = DateTime.UtcNow;
        public string Token { get; set; }   
    }
}
