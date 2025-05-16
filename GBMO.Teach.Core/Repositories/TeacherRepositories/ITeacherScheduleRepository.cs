using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;

namespace GBMO.Teach.Core.Repositories.TeacherRepositories
{
    public interface ITeacherScheduleRepository : IGenericRepository<TeacherSchedule>
    {
        Task<List<TeacherSchedule>> GetTeacherAllClassesByTeacherIdAsync(
            Guid teacherId,
            bool onlyActives = false,
            CancellationToken cancellationToken = default);

        Task<List<TeacherSchedule>> GetTeacherClassHistoryByTeacherIdAsync(
            Guid teacherId,
            CancellationToken cancellationToken = default);
    }
}
