using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Common;

namespace GBMO.Teach.Core.Entities.Teachers;

public class Teacher : BaseEntity
{
    public Guid UserId { get; set; }
    public decimal? HourlyRate { get; set; }
    public string? Bio { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<TeacherSchedule> TeacherSchedules { get; set; }
    public virtual ICollection<TeacherStudentConnection> TeacherStudentConnections { get; set; }

}