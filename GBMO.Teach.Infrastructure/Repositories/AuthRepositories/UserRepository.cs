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

        public async Task<List<User>> GetNotConnectedTeachersAsync(string studentId)
        {
            return await _gbmoDbContext.Users.Where(c=>c.RoleTypeId == (int)RoleTypes.Teacher)
                .Include(c=>c.Teacher)
                .ThenInclude(c=>c.TeacherStudentConnections)
                .Where(c=> !c.Teacher.TeacherSchedules.Any(x => x.StudentId.Equals(Guid.Parse(studentId))))
                .ToListAsync(); 
        }
    }
}
