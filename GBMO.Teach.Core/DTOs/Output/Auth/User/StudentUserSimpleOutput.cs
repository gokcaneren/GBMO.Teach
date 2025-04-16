namespace GBMO.Teach.Core.DTOs.Output.Auth.User
{
    public class StudentUserSimpleOutput
    {
        public Guid UserId { get; set; }
        public Guid StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleTypeId { get; set; }
    }
}
