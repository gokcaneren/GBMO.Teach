namespace GBMO.Teach.Core.DTOs.Output.Teacher
{
    public class SimpleTeacherOutput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
