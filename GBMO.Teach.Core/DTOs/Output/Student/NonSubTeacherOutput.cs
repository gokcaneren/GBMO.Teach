namespace GBMO.Teach.Core.DTOs.Output.Student
{
    public class NonSubTeacherOutput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public Guid TeacherId { get; set; }
        public decimal? HourlyRate { get; set; }
        public string? Bio { get; set; }
    }
}
