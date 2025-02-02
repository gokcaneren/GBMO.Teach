using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Common;

public class ClassBooking  : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public ClassStatus Status { get; set; }

    public virtual Teacher Teacher { get; set; }
    public virtual Student Student { get; set; }
}