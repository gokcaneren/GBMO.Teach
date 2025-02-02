using GBMO.Teach.Core.Entities.Common;

namespace GBMO.Teach.Core.Entities.Students;

public class Student : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public virtual ICollection<ClassBooking> ClassBookings { get; set; }
    public virtual ICollection<TeacherStudentConnection> TeacherStudentConnections { get; set; }
}