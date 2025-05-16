using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Infrastructure.Context;
using GBMO.Teach.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GBMO.Teach.Infrastructure.Repositories.TeacherRepositories
{
    public class TeacherScheduleRepository : GenericRepository<TeacherSchedule>, ITeacherScheduleRepository
    {
        public TeacherScheduleRepository(GbmoDbContext gbmoDbContext) : base(gbmoDbContext)
        {
        }

        public async Task<List<TeacherSchedule>> GetTeacherAllClassesByTeacherIdAsync(Guid teacherId,
            bool onlyActives = false, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.TeacherSchedules
                .Include(c => c.Student)
                .ThenInclude(c => c.User)
                .Where(c => c.TeacherId.Equals(teacherId))
                .WhereIf(onlyActives, c => c.ClassStatusses == ClassStatusses.Booked)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<TeacherSchedule>> GetTeacherClassHistoryByTeacherIdAsync(Guid teacherId, CancellationToken cancellationToken = default)
        {
            return await _gbmoDbContext.TeacherSchedules
                .Include(c => c.Student)
                .ThenInclude(c => c.User)
                .Where(c=> c.TeacherId.Equals(teacherId) && c.ClassStatusses == ClassStatusses.Completed)
                .ToListAsync(cancellationToken);
        }
    }
}
