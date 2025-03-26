using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Teachers;

public class TeacherSchedule : BaseEntity
{
    public Guid TeacherId { get; set; }
    public DateTime ClassStartDate { get; set; }
    public DateTime ClassEndDate { get; set; }
    public bool IsBooked { get; set; }
    public virtual Teacher Teacher { get; set; }
}