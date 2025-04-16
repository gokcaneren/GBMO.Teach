using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Core.Entities.Students;

public class Student : BaseEntity
{
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<TeacherSchedule> TeacherSchedules { get; set; }
    public virtual ICollection<TeacherStudentConnection> TeacherStudentConnections { get; set; }
}