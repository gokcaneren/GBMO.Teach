using AutoMapper;
using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Common;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Services.CommonServices;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Application.Services.CommonServices
{
    public class SubRequestService : Service<SubsRequest>, ISubRequestService
    {
        private readonly ISubRequestRepository _subRequestRepository;
        private readonly IAuthService _authService;
        private readonly ITeacherStudentConnectionRepository _teacherStudentConnectionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public SubRequestService(IGenericRepository<SubsRequest> repository,
            ISubRequestRepository subRequestRepository,
            IAuthService authService,
            ITeacherStudentConnectionRepository teacherStudentConnectionRepository,
            IStringLocalizer<SharedResources> localizer,
            IUserRepository userRepository,
            IMapper mapper) : base(repository)
        {
            _subRequestRepository = subRequestRepository;
            _authService = authService;
            _teacherStudentConnectionRepository = teacherStudentConnectionRepository;
            _localizer = localizer;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<NonSubTeacherOutput>>> GetNonSubTeachersAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId))
            {
                return await Task.FromResult(ApiResponse<List<NonSubTeacherOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null));
            }

            var currentUser = await _userRepository.GetByAsync(c=>c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Student, cancellationToken);

            if (currentUser!.RoleTypeId == (int)RoleTypes.Teacher)
            {
                return await Task.FromResult(ApiResponse<List<NonSubTeacherOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null));
            }

            var teachers = await _userRepository.GetNotConnectedTeachersAsync(currentUser.Student.Id.ToString(), cancellationToken);

            var notRequestedTeachers = await Task.Run(() => teachers.Select(teacher =>
            {
                var isRequested =  _subRequestRepository.GetBy(x => x.TeacherId.Equals(teacher.Teacher.Id) &&
                x.StudenId.Equals(currentUser.Student.Id) && (x.Status == SubRequestStatusses.Sent || x.Status == SubRequestStatusses.Accepted));
                return isRequested == null ? teacher : null;
            }).Where(x => x != null).ToList());

            var nonSubTeachers = _mapper.Map<List<NonSubTeacherOutput>>(notRequestedTeachers);

            return await Task.FromResult(ApiResponse<List<NonSubTeacherOutput>>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], nonSubTeachers));
        }

        public async Task<ApiResponse<bool>> SendSubRequestAsync(string teacherId, CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], false));
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Student, cancellationToken);

            if (await IsAlreadySent(currentUser.Student.Id, Guid.Parse(teacherId)))
            {
                return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.SubReqAlredySent"], false));
            }

            if (await TeacherStudentConnectionIsExist(currentUser.Student.Id, Guid.Parse(teacherId)))
            {
                return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.SubErrAlreadyConnected"], false));
            }

            var newSubRequest = new SubsRequest()
            {
                StudenId = currentUser.Student.Id,
                TeacherId = Guid.Parse(teacherId),
                Status = SubRequestStatusses.Sent
            };

            await _subRequestRepository.CreateAsync(newSubRequest, autoSave: true, cancellationToken: cancellationToken);

            return await Task.FromResult(ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true));
        }

        public async Task<bool> TeacherStudentConnectionIsExist(Guid studentId, Guid teacherId)
        {
            var connection = await _teacherStudentConnectionRepository.GetByAsync(c => c.StudentId.Equals(studentId) &&
            c.TeacherId.Equals(teacherId));

            return connection != null;
        }

        public async Task<bool> IsAlreadySent(Guid studentId, Guid teacherId)
        {
            var isSent = await _subRequestRepository.GetByAsync(c => c.StudenId.Equals(studentId)
            && c.TeacherId.Equals(teacherId) && c.Status == SubRequestStatusses.Sent);

            return isSent != null;
        }

    }
}
