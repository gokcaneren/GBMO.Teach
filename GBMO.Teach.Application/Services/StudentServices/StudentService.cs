using AutoMapper;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Output.Teacher;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Core.Services.StudentServices
{
    public class StudentService : Service<Student>, IStudentService
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ITeacherScheduleRepository _teacherScheduleRepository;
        private readonly ITeacherStudentConnectionRepository _teacherStudenctConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IGenericRepository<Student> repository,
            IAuthService authService,
            IUserRepository userRepository,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer,
            ITeacherScheduleRepository teacherScheduleRepository,
            ITeacherStudentConnectionRepository teacherStudenctConnectionRepository,
            IUnitOfWork unitOfWork) : base(repository)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;
            _localizer = localizer;
            _teacherScheduleRepository = teacherScheduleRepository;
            _teacherStudenctConnectionRepository = teacherStudenctConnectionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<List<SimpleTeacherOutput>>> GetSubbedTeachersAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return await Task.FromResult(ApiResponse<List<SimpleTeacherOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null));
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Student, cancellationToken);

            var connectedTeachers = await _userRepository
                .GetConnectedTeachersAsync(currentUser.Student.Id.ToString(), cancellationToken);

            var simpleTeacherList = _mapper.Map<List<SimpleTeacherOutput>>(connectedTeachers);

            return await Task.FromResult(ApiResponse<List<SimpleTeacherOutput>>.SuccessResponse(HttpStatusCode.OK,
                _localizer["Gnrl.Successful"], simpleTeacherList));
        }

        public async Task<ApiResponse<bool>> UnSubTeacherAsync(string teacherId, CancellationToken cancellationToken = default)
        {
            try
            {
                var currentUserId = _authService?.GetCurrentUserId();

                if (string.IsNullOrEmpty(currentUserId))
                {
                    return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false));
                }

                var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

                await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Student, cancellationToken);

                var connection = await _teacherStudenctConnectionRepository.GetByAsync(c =>
                c.StudentId.Equals(currentUser.Student.Id) && c.TeacherId.Equals(Guid.Parse(teacherId)), cancellationToken);

                if (connection == null)
                {
                    return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["TcStCon.TeacherAlreadyNotConnected"], false));
                }

                if (await IsHasActiveClass(teacherId, currentUser))
                {
                    return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["TcStCon.TcherStdntHasNotCompletedClass"], false));
                }

                await _teacherStudenctConnectionRepository.DeleteAsync(connection, cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);

                return await Task.FromResult(ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> IsHasActiveClass(string teacherId, User? currentUser)
        {
            return await _teacherScheduleRepository.GetByAsync(c =>
                        c.TeacherId.Equals(Guid.Parse(teacherId)) && c.StudentId.Equals(currentUser.Student.Id)
                        && c.ClassStatusses == Enums.ClassStatusses.Booked) != null;
        }
    }
}
