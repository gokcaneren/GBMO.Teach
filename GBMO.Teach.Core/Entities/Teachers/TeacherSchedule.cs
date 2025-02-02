using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Teachers;

public class TeacherSchedule : BaseEntity
{
    public Guid TeacherId { get; set; }
    public DaysOfWeek DayOfWeek { get; set; } 
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int ClassDurationMinutes { get; set; }

    public virtual Teacher Teacher { get; set; }
}