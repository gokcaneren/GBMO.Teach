using AutoMapper;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Output.Teacher;
using GBMO.Teach.Core.Entities.Students;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
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
        public StudentService(IGenericRepository<Student> repository,
            IAuthService authService,
            IUserRepository userRepository,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(repository)
        {
            _authService = authService;
            _userRepository = userRepository;
            _mapper = mapper;
            _localizer = localizer;
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
    }
}
