using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule
{
    public class TeacherScheduleCreateInput
    {
        public DateTime ClassStartDate { get; set; }
        public DateTime ClassEndDate { get; set; }
    }
}
