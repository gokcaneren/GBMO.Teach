using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Teachers;

public class TeacherSchedule : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Guid? StudentId { get; set; }
    public DateTime ClassStartDate { get; set; }
    public DateTime ClassEndDate { get; set; }
    public ClassStatusses ClassStatusses { get; set; }
    public virtual Teacher Teacher { get; set; }
    public virtual Student Student { get; set; }
}