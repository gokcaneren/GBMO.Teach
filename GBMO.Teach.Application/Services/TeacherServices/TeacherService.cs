﻿using AutoMapper;
using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Common;
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
using Microsoft.Extensions.Logging;
using System.Net;

namespace GBMO.Teach.Core.Services.TeacherServices
{
    public class TeacherService : Service<Teacher>, ITeacherService
    {

        private readonly ISubRequestRepository _subRequestRepository;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherStudentConnectionRepository _teacherStudentConnectionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ILogger<TeacherService> _logger;
        private readonly ITeacherScheduleRepository _teacherScheduleRepository;

        public TeacherService(IGenericRepository<Teacher> repository,
            ISubRequestRepository subRequestRepository,
            IMapper mapper,
            IAuthService authService,
            IStringLocalizer<SharedResources> localizer,
            IUserRepository userRepository,
            ITeacherStudentConnectionRepository teacherStudentConnectionRepository,
            IUnitOfWork unitOfWork,
            ILogger<TeacherService> logger,
            ITeacherScheduleRepository teacherScheduleRepository) : base(repository)
        {
            _subRequestRepository = subRequestRepository;
            _mapper = mapper;
            _authService = authService;
            _localizer = localizer;
            _userRepository = userRepository;
            _teacherStudentConnectionRepository = teacherStudentConnectionRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _teacherScheduleRepository = teacherScheduleRepository;
        }

        public async Task<ApiResponse<bool>> ActSubRequestAsync(string studentId, bool isAccepted = false,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var currentUserId = _authService.GetCurrentUserId();

                if (string.IsNullOrEmpty(currentUserId))
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false);
                }

                var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

                await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher, cancellationToken);


                if(await TeacherStudentConnectionIsExist(Guid.Parse(studentId), currentUser.Teacher.Id))
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["TcStCon.TeachErrAlreadyConnected"], false);
                }

                var subRequest = await _subRequestRepository.GetByAsync(c => c.StudenId.Equals(Guid.Parse(studentId)));

                if (subRequest == null)
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false);
                }

                if (isAccepted)
                {
                    var newConnection = new TeacherStudentConnection()
                    {
                        TeacherId = currentUser.Teacher.Id,
                        StudentId = Guid.Parse(studentId)
                    };

                    await _teacherStudentConnectionRepository.CreateAsync(newConnection, false, cancellationToken);

                    subRequest.Status = Enums.RequestStatusses.Accepted;

                    await _subRequestRepository.UpdateAsync(subRequest, false, cancellationToken);

                    await _unitOfWork.CommitAsync(cancellationToken);

                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.OK,
                        _localizer["Gnrl.Successful"], true);
                }

                subRequest.Status = Enums.RequestStatusses.Rejected;

                await _subRequestRepository.UpdateAsync(subRequest, true, cancellationToken);

                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.OK,
                        _localizer["Gnrl.Successful"], true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ApiResponse<List<TeacherClassOutput>>> GetAllClassesAsync(bool onlyActives = false, 
            CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return ApiResponse<List<TeacherClassOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null);
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)), cancellationToken);

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher, cancellationToken);

            var activeClasses = await _teacherScheduleRepository.GetTeacherAllClassesByTeacherIdAsync(currentUser.Teacher.Id,
                onlyActives, cancellationToken);

            var activeClassesOutput = _mapper.Map<List<TeacherClassOutput>>(activeClasses);

            return ApiResponse<List<TeacherClassOutput>>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], activeClassesOutput);
        }

        public async Task<ApiResponse<List<TeacherClassOutput>>> GetClassHistoryAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return ApiResponse<List<TeacherClassOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null);
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)), cancellationToken);

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher, cancellationToken);

            var classHistory = await _teacherScheduleRepository.GetTeacherClassHistoryByTeacherIdAsync(currentUser.Teacher.Id, cancellationToken);

            var classHistoryOutput = _mapper.Map<List<TeacherClassOutput>>(classHistory);

            return ApiResponse<List<TeacherClassOutput>>.SuccessResponse(HttpStatusCode.OK,
                _localizer["Gnrl.Successful"], classHistoryOutput);
        }
        public async Task<ApiResponse<List<StudentUserSimpleOutput>>> GetSubRequestListAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return ApiResponse<List<StudentUserSimpleOutput>>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null);
            }

            var currentUser = await _userRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(currentUserId)));

            await _userRepository.LoadNavigationPropertyAsync(currentUser, c => c.Teacher, cancellationToken);

            var subRequestList = await _subRequestRepository.GetListByAsync(c=>c.TeacherId.Equals(currentUser.Teacher.Id) &&
            c.Status == Enums.RequestStatusses.Sent, cancellationToken);

            var requesterStudents = await _userRepository.GetRequesterStudentsAsync(subRequestList.Select(c => c.StudenId).ToList(),
                cancellationToken);

            var simpleRequesterStudentList = _mapper.Map<List<StudentUserSimpleOutput>>(requesterStudents);

            return ApiResponse<List<StudentUserSimpleOutput>>.SuccessResponse(HttpStatusCode.OK,
                _localizer["Gnrl.Successful"], simpleRequesterStudentList);
        }

        private async Task<bool> TeacherStudentConnectionIsExist(Guid studentId, Guid teacherId)
        {
            var connection = await _teacherStudentConnectionRepository.GetByAsync(c => c.StudentId.Equals(studentId) &&
            c.TeacherId.Equals(teacherId));

            return connection != null;
        }
    }
}
