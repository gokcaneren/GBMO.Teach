using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GBMO.Teach.Infrastructure.Repositories.AuthRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }

        public async Task<List<User>> GetConnectedTeachersAsync(string studentId, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Users.Where(c => c.RoleTypeId == (int)RoleTypes.Teacher)
                .Include(c => c.Teacher)
                .ThenInclude(c => c.TeacherStudentConnections)
                .Where(c => c.Teacher.TeacherStudentConnections.Any(x => x.StudentId.Equals(Guid.Parse(studentId))))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<User>> GetNotConnectedTeachersAsync(string studentId,
            CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Users.Where(c=>c.RoleTypeId == (int)RoleTypes.Teacher)
                .Include(c=>c.Teacher)
                .ThenInclude(c=>c.TeacherStudentConnections)
                .Where(c=> !c.Teacher.TeacherStudentConnections.Any(x => x.StudentId.Equals(Guid.Parse(studentId))))
                .ToListAsync(cancellationToken); 
        }

        public async Task<List<User>> GetRequesterStudentsAsync(List<Guid> studentIds,
            CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.Users.Where(c => c.RoleTypeId == (int)RoleTypes.Student ||
            c.RoleTypeId == (int)RoleTypes.Parent)
                .Include(c => c.Student)
                .Where(c => studentIds.Contains(c.Student.Id))
                .ToListAsync(cancellationToken);
        }
    }
}
