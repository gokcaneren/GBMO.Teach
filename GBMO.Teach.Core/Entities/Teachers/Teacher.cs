using GBMO.Teach.Core.Entities.Common;

namespace GBMO.Teach.Core.Entities.Teachers;

public class Teacher : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? Bio { get; set; }
    
    public virtual ICollection<ClassBooking> ClassBookings { get; set; }
    public virtual ICollection<TeacherSchedule> TeacherSchedules { get; set; }
    public virtual ICollection<TeacherStudentConnection> TeacherStudentConnections { get; set; }

    public Teacher()
    {
        
    }

    public Teacher(string firstName, string lastName, string email, string passwordHash, string? bio)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Bio = bio;
    }
}