using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Core.Repositories.StudentRepositories;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Application.Services.CommonServices
{
    public class ScheduleBookRequestService : Service<ScheduleBookRequest>, IScheduleBookRequestService
    {
        private readonly IScheduleBookRequestRepository _scheduleBookRequestRepository;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherStudentConnectionRepository _teacherStudentConnectionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherScheduleRepository _teacherScheduleRepository;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ScheduleBookRequestService(IGenericRepository<ScheduleBookRequest> repository,
            IScheduleBookRequestRepository scheduleBookRequestRepository,
            IAuthService authService,
            IUserRepository userRepository,
            ITeacherStudentConnectionRepository teacherStudentConnectionRepository,
            IStringLocalizer<SharedResources> localizer,
            IStudentRepository studentRepository,
            ITeacherScheduleRepository teacherScheduleRepository)
            : base(repository)
        {
            _scheduleBookRequestRepository = scheduleBookRequestRepository;
            _authService = authService;
            _userRepository = userRepository;
            _teacherStudentConnectionRepository = teacherStudentConnectionRepository;
            _localizer = localizer;
            _studentRepository = studentRepository;
            _teacherScheduleRepository = teacherScheduleRepository;
        }

        public async Task<ApiResponse<bool>> SendClassBookRequestAsync(string teacherId, string scheduleId, CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], false);
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Student, cancellationToken);

            if (!await TeacherStudentConnectionIsExist(currentUser.Student.Id, Guid.Parse(teacherId)))
            {
                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.TeacherAlreadyNotConnected"], false);
            }

            if (await IsAlreadySent(currentUser.Student.Id, Guid.Parse(teacherId)))
            {
                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.SubReqAlredySent"], false);
            }

            var scheduleBookRequest = new ScheduleBookRequest
            {
                StudenId = currentUser.Student.Id,
                TeacherId = Guid.Parse(teacherId),
                ScheduleId = Guid.Parse(scheduleId),
            };

            await _scheduleBookRequestRepository.CreateAsync(scheduleBookRequest, autoSave: true, cancellationToken);

            return ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true);
        }

        public async Task<ApiResponse<List<StudentWithScheduleOutput>>> GetClassBookRequestsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var currentUserId = _authService.GetCurrentUserId();

                if (string.IsNullOrEmpty(currentUserId))
                {
                    return ApiResponse<List<StudentWithScheduleOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], null);
                }

                var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

                await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher, cancellationToken);

                var scheduleBookRequests = await _scheduleBookRequestRepository.GetListByAsync(c =>
                c.TeacherId.Equals(currentUser.Teacher.Id) && c.Status == RequestStatusses.Sent);

                var studentOutputs = new List<StudentWithScheduleOutput>();

                var studentBookRequests = scheduleBookRequests.GroupBy(r => r.StudenId);

                foreach (var bookRequest in studentBookRequests)
                {
                    var student = await _studentRepository.GetByAsync(x => x.Id == bookRequest.Key, cancellationToken);
                    await _studentRepository.LoadNavigationPropertyAsync(student, s => s.User, cancellationToken);

                    var scheduleOutputs = new List<TeacherScheduleOutput>();

                    foreach (var request in bookRequest)
                    {
                        var schedule = await _teacherScheduleRepository.GetByAsync(x => x.Id == request.ScheduleId, cancellationToken);
                        scheduleOutputs.Add(new TeacherScheduleOutput
                        {
                            ScheduleId = schedule.Id,
                            ClassStartDate = schedule.ClassStartDate,
                            ClassEndDate = schedule.ClassEndDate,
                            ClassStatusses = schedule.ClassStatusses
                        });
                    }

                    studentOutputs.Add(new StudentWithScheduleOutput
                    {
                        StudentId = student.Id,
                        FirstName = student.User.FirstName,
                        LastName = student.User.LastName,
                        Email = student.User.Email,
                        ClassSchedule = scheduleOutputs
                    });
                }

                return ApiResponse<List<StudentWithScheduleOutput>>.SuccessResponse(HttpStatusCode.OK,
                        _localizer["Gnrl.Successful"], studentOutputs);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsAlreadySent(Guid studentId, Guid teacherId)
        {
            var isSent = await _scheduleBookRequestRepository.GetByAsync(c => c.StudenId.Equals(studentId)
            && c.TeacherId.Equals(teacherId) && c.Status == RequestStatusses.Sent);

            return isSent != null;
        }

        public async Task<bool> TeacherStudentConnectionIsExist(Guid studentId, Guid teacherId)
        {
            var connection = await _teacherStudentConnectionRepository.GetByAsync(c => c.StudentId.Equals(studentId) &&
            c.TeacherId.Equals(teacherId));

            return connection != null;
        }
    }
}
