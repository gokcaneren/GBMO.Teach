using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule
{
    public class TeacherClassOutput
    {
        public Guid? StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
        public ClassStatusses ClassStatusses { get; set; }
    }
}
