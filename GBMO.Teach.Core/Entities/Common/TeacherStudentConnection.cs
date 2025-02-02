using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Core.Entities.Common;

public class TeacherStudentConnection
{
    public Guid ConnectionId { get; set; }
    public Guid TeacherId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime ConnectedAt { get; set; } = DateTime.Now;
    
    public virtual Teacher Teacher { get; set; }
    public virtual Student Student { get; set; }
}