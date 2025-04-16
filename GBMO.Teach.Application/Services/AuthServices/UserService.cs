using AutoMapper;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Application.Utilities;
using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.AuthRepositories;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Net;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public class UserService : Service<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private IStringLocalizer<SharedResources> _localizer;

        public UserService(
            IGenericRepository<User> repository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IAuthService authService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer
            )
            : base(repository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
            _authService = authService;
        }

        public async Task<ApiResponse<UserLoginOutput>> LoginAsync(UserLoginInput userLoginInput, CancellationToken cancellationToken = default)
        {
            var existedUser = await _userRepository.GetByAsync(c => c.Email.Equals(userLoginInput.Email), cancellationToken);

            if (existedUser == null)
            {
                return await Task.FromResult(ApiResponse<UserLoginOutput>
                    .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Auth.UserNotExistOrWrongPassword"], null));
            }

            var result = PasswordManager.Verify(userLoginInput.Password, existedUser.PasswordHash);

            if (result == false)
            {
                return await Task.FromResult(ApiResponse<UserLoginOutput>
                    .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Auth.UserNotExistOrWrongPassword"], null));
            }

            var token = _authService.GenerateToken(existedUser);

            var userLoginResponse = _mapper.Map<UserLoginOutput>(existedUser);
            userLoginResponse.Token = token;

            return await Task.FromResult(ApiResponse<UserLoginOutput>.
                SuccessResponse(HttpStatusCode.OK, _localizer["Auth.UserSuccessfulLogin"], userLoginResponse));
        }

        public async Task<ApiResponse<bool>> RegisterAsync(UserRegisterInput userRegisterInput, CancellationToken cancellationToken = default)
        {
            var userIsExist = await _userRepository.GetByAsync(c => c.Email.Equals(userRegisterInput.Email), cancellationToken);

            if (userIsExist != null)
            {
                return await Task.FromResult(ApiResponse<bool>
                    .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Auth.UserAlreadyExist"], false));
            }

            var userEntity = _mapper.Map<User>(userRegisterInput);

            await _userRepository.CreateAsync(userEntity, autoSave: true, cancellationToken: cancellationToken);

            return await Task.FromResult(ApiResponse<bool>
                .SuccessResponse(HttpStatusCode.Created, _localizer["Auth.UserCreateSuccessful"], true));
        }

        public async Task<ApiResponse<bool>> UpdateTeacherProfileAsync(UpdateTeacherProfileInput updateTeacherProfileInput, CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return await Task.FromResult(ApiResponse<bool>
                .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Usr.TeacherProfileUpdateError"], true));
            }

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userId), cancellationToken);

            if (user == null)
            {
                return await Task.FromResult(ApiResponse<bool>
                .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Usr.TeacherProfileUpdateError"], true));
            }

            if (!IsTeacher(user))
            {
                return await Task.FromResult(ApiResponse<bool>
                .ErrorResponse(HttpStatusCode.BadRequest, _localizer["Usr.TeacherProfileUpdateError"], true));
            }

            await _userRepository.LoadNavigationPropertyAsync(user, u => u.Teacher, cancellationToken);

            user.Teacher.Bio = updateTeacherProfileInput.Bio;
            user.Teacher.HourlyRate = updateTeacherProfileInput.HourlyRate;

            await _userRepository.UpdateAsync(user, autoSave: true, cancellationToken: cancellationToken);

            return await Task.FromResult(ApiResponse<bool>
                .SuccessResponse(HttpStatusCode.OK, _localizer["Usr.TeacherProfileUpdateSuccessful"], true));
        }

        private bool IsTeacher(User user)
        {
            return user.RoleTypeId is ((int)RoleTypes.Teacher) or ((int)RoleTypes.Parent);
        }
    }
}
