namespace GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule
{
    public class TeacherWithScheduleOutput
    {
        public Guid? TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<TeacherScheduleOutput> ClassSchedule { get; set; }
    }
}
