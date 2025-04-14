using AutoMapper;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Student;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public class TeacherService : Service<Teacher>, ITeacherService
    {

        private readonly ISubRequestRepository _subRequestRepository;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public TeacherService(IGenericRepository<Teacher> repository,
            ISubRequestRepository subRequestRepository,
            IMapper mapper,
            IAuthService authService,
            IStringLocalizer<SharedResources> localizer,
            IUserRepository userRepository) : base(repository)
        {
            _subRequestRepository = subRequestRepository;
            _mapper = mapper;
            _authService = authService;
            _localizer = localizer;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse<List<UserSimpleOutput>>> GetSubRequestListAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return await Task.FromResult(ApiResponse<List<UserSimpleOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null));
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher);

            var subRequestList = await _subRequestRepository.GetListByAsync(c=>c.TeacherId.Equals(currentUser.Teacher.Id) &&
            c.Status == Enums.SubRequestStatusses.Sent, cancellationToken);

            var requesterStudents = await _userRepository.GetRequesterStudentsAsync(subRequestList.Select(c => c.StudenId).ToList(),
                cancellationToken);

            var simpleRequesterStudentList = _mapper.Map<List<UserSimpleOutput>>(requesterStudents);

            return await Task.FromResult(ApiResponse<List<UserSimpleOutput>>.SuccessResponse(HttpStatusCode.OK,
                _localizer["Gnrl.Successful"], simpleRequesterStudentList));
        }
    }
}
