using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.DTOs.Input.Auth.User
{
    public class UserRegisterInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public PublicRole RoleTypeId { get; set; }
        public UserRegisterTeacherDetailInput? UserTeacherDetail { get; set; }
    }
}
