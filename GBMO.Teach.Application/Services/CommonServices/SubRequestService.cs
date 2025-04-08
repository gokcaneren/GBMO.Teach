using AutoMapper;
using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Entities.Common;
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

        public async Task<ApiResponse<List<NonSubTeacher>>> GetNonSubTeachersAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();
            if (string.IsNullOrEmpty(currentUserId))
            {
                return await Task.FromResult(ApiResponse<List<NonSubTeacher>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null));
            }

            var teachers = await _userRepository.GetNotConnectedTeachersAsync(currentUserId);

            var nonSubTeachers = _mapper.Map<List<NonSubTeacher>>(teachers);

            return await Task.FromResult(ApiResponse<List<NonSubTeacher>>.SuccessResponse(HttpStatusCode.OK,
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

            if (await TeacherStudentConnectionIsExist(currentUserId, teacherId))
            {
                return await Task.FromResult(ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.SubErrAlreadyConnected"], false));
            }

            var newSubRequest = new SubsRequest()
            {
                StudenId = Guid.Parse(currentUserId),
                TeacherId = Guid.Parse(teacherId),
                Status = SubRequestStatusses.Sent
            };

            await _subRequestRepository.CreateAsync(newSubRequest, autoSave: true, cancellationToken: cancellationToken);

            return await Task.FromResult(ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true));
        }

        public async Task<bool> TeacherStudentConnectionIsExist(string studentId, string teacherId)
        {
            var connection = await _teacherStudentConnectionRepository.GetByAsync(c => c.StudentId.Equals(Guid.Parse(studentId)) &&
            c.TeacherId.Equals(Guid.Parse(teacherId)));

            return connection != null;
        }
    }
}
