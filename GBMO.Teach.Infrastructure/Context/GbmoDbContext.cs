using System.Reflection;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Entities.Configurations;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;
using Microsoft.EntityFrameworkCore;

namespace GBMO.Teach.Infrastructure.Context;

public class GbmoDbContext : DbContext
{
    public GbmoDbContext(DbContextOptions<GbmoDbContext> options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherSchedule> TeacherSchedules { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<TeacherStudentConnection> TeacherStudentConnections { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<SubsRequest> SubsRequests { get; set; }
    public DbSet<ScheduleBookRequest> ScheduleBookRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}