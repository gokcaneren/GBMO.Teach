namespace GBMO.Teach.Core.Entities;

public class TeacherStudentConnection
{
    public int ConnectionId { get; set; }
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public DateTime ConnectedAt { get; set; } = DateTime.Now;
    
    public Teacher.Teacher Teacher { get; set; }
    public Student.Student Student { get; set; }
}