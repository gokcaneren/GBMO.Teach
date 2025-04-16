using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public class TeacherScheduleService : Service<TeacherSchedule>, ITeacherScheduleService
    {
        private readonly ITeacherScheduleRepository _teacherScheduleRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public TeacherScheduleService(IGenericRepository<TeacherSchedule> repository, ITeacherScheduleRepository teacherScheduleRepository, ITeacherRepository teacherRepository, IAuthService authService, IStringLocalizer<SharedResources> localizer, IUnitOfWork unitOfWork) : base(repository)
        {
            _teacherScheduleRepository = teacherScheduleRepository;
            _teacherRepository = teacherRepository;
            _authService = authService;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<bool>> CreateClassScheduleAsync(TeacherScheduleCreateInput teacherScheduleCreateInput,
            CancellationToken cancellationToken = default)
        {

            var userId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], false));
            }

            try
            {
                var teacher = await _teacherRepository.GetByAsync(c => c.UserId.Equals(Guid.Parse(userId)));

                if (teacher == null)
                {
                    return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false));
                }

                await _teacherRepository.LoadNavigationPropertyAsync(teacher, c => c.TeacherSchedules);


                if (teacher.TeacherSchedules.Any(c =>
                    (teacherScheduleCreateInput.ClassStartDate < c.ClassEndDate &&
                    teacherScheduleCreateInput.ClassEndDate > c.ClassStartDate)))
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest, _localizer["Gnrl.SmtError"], false);
                }

                var teacherSchedule = new TeacherSchedule
                {
                    TeacherId = teacher.Id,
                    ClassStartDate = teacherScheduleCreateInput.ClassStartDate.ToUniversalTime(),
                    ClassEndDate = teacherScheduleCreateInput.ClassEndDate.ToUniversalTime(),
                };

                await _teacherScheduleRepository.CreateAsync(teacherSchedule, autoSave: true, cancellationToken: cancellationToken);

                return await Task.FromResult(ApiResponse<bool>.SuccessResponse(HttpStatusCode.Created,
                    _localizer["Tchr.ClassCreatedSuccessful"], true));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} {ex.StackTrace}");
            }
            
        }
    }
}
