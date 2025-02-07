using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Core.Entities.Common;

public class TeacherStudentConnection : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime ConnectionTime { get; set; } = DateTime.UtcNow;
    
    public virtual Teacher Teacher { get; set; }
    public virtual Student Student { get; set; }
}