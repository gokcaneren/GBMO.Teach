﻿using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule;
using GBMO.Teach.Core.DTOs.Input.Teacher.TeacherSchedule.BookRequest;
using GBMO.Teach.Core.DTOs.Output.Teacher.TeacherSchedule;
using GBMO.Teach.Core.Entities.Teachers;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Repositories.CommonRepositories;
using GBMO.Teach.Core.Repositories.TeacherRepositories;
using GBMO.Teach.Core.Services.AuthServices;
using GBMO.Teach.Core.UnitOfWorks;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Infrastructure.Extensions;
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
        private readonly IScheduleBookRequestRepository _scheduleBookRequestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public TeacherScheduleService(
            IGenericRepository<TeacherSchedule> repository,
            ITeacherScheduleRepository teacherScheduleRepository,
            ITeacherRepository teacherRepository,
            IAuthService authService,
            IStringLocalizer<SharedResources> localizer,
            IUnitOfWork unitOfWork,
            IScheduleBookRequestRepository scheduleBookRequestRepository) : base(repository)
        {
            _teacherScheduleRepository = teacherScheduleRepository;
            _teacherRepository = teacherRepository;
            _authService = authService;
            _localizer = localizer;
            _unitOfWork = unitOfWork;
            _scheduleBookRequestRepository = scheduleBookRequestRepository;
        }

        public async Task<ApiResponse<bool>> ActBookRequestAsync(bool isAccepted, BookRequestInput bookRequestInput, CancellationToken cancellationToken = default)
        {
            var userId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], false);
            }

            try
            {
                var teacher = await _teacherRepository.GetByAsync(c => c.UserId.Equals(Guid.Parse(userId)));

                if (teacher == null)
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false);
                }

                var teacherSchedule = await _teacherScheduleRepository.GetByAsync(c => c.Id.Equals(Guid.Parse(bookRequestInput.ScheduleId)), cancellationToken);

                //await _teacherRepository.LoadNavigationPropertyAsync(teacher, c => c.TeacherSchedules, cancellationToken);

                var bookRequest = await _scheduleBookRequestRepository.GetByAsync(c => c.TeacherId.Equals(teacher.Id)
                    && c.StudenId.Equals(Guid.Parse(bookRequestInput.StudentId)) &&
                    c.ScheduleId.Equals(Guid.Parse(bookRequestInput.ScheduleId)), cancellationToken);


                if (isAccepted)
                {
                    teacherSchedule.StudentId = bookRequest.StudenId;
                    bookRequest.Status = RequestStatusses.Accepted;

                    var otherBookRequests = await _scheduleBookRequestRepository.GetListByAsync(
                        c => c.ScheduleId.Equals(Guid.Parse(bookRequestInput.ScheduleId)) &&
                        c.StudenId != bookRequest.StudenId);

                    foreach (var otherBookRequest in otherBookRequests)
                    {
                        otherBookRequest.Status = RequestStatusses.Rejected;
                    }

                    await _unitOfWork.CommitAsync(cancellationToken);

                    return ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true);

                }
                else
                {
                    bookRequest.Status = RequestStatusses.Rejected;
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return ApiResponse<bool>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], true);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ApiResponse<bool>> CreateClassScheduleAsync(TeacherScheduleCreateInput teacherScheduleCreateInput,
            CancellationToken cancellationToken = default)
        {

            var userId = _authService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], false);
            }

            try
            {
                var teacher = await _teacherRepository.GetByAsync(c => c.UserId.Equals(Guid.Parse(userId)));

                if (teacher == null)
                {
                    return ApiResponse<bool>.ErrorResponse(HttpStatusCode.BadRequest,
                        _localizer["Gnrl.SmtError"], false);
                }

                await _teacherRepository.LoadNavigationPropertyAsync(teacher, c => c.TeacherSchedules, cancellationToken);


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

                return ApiResponse<bool>.SuccessResponse(HttpStatusCode.Created,
                    _localizer["Tchr.ClassCreatedSuccessful"], true);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} {ex.StackTrace}");
            }
            
        }

        public async Task<ApiResponse<TeacherWithScheduleOutput>> GetTeacherClassScheduleAsync(string teacherId, CancellationToken cancellationToken = default)
        {
            var teacherSchedule = await _teacherScheduleRepository.GetListByAsync(c => c.TeacherId.Equals(Guid.Parse(teacherId))
                && c.ClassStatusses == Enums.ClassStatusses.NotBooked);

            if (teacherSchedule.IsNullOrEmpty())
            {
                return ApiResponse<TeacherWithScheduleOutput>.ErrorResponse(HttpStatusCode.NotFound,
                    _localizer["TchrSch.TeacherDoesntHaveAvailableClass"], null);
            }

            var teacher = await _teacherRepository.GetByAsync(c=> c.Id.Equals(Guid.Parse(teacherId)));

            if (teacher == null)
            {
                return ApiResponse<TeacherWithScheduleOutput>.ErrorResponse(HttpStatusCode.BadRequest,
                    _localizer["Gnrl.SmtError"], null);
            }

            await _teacherRepository.LoadNavigationPropertyAsync(teacher, c => c.User);


            var teacherWithScheduleOutput = new TeacherWithScheduleOutput
            {
                TeacherId = teacher.Id,
                FirstName = teacher.User.FirstName,
                LastName = teacher.User.LastName,
                Email = teacher.User.Email,
                ClassSchedule = teacherSchedule.Select(c => new TeacherScheduleOutput
                {
                    ScheduleId = c.Id,
                    ClassStartDate = c.ClassStartDate,
                    ClassEndDate = c.ClassEndDate,
                    ClassStatusses = c.ClassStatusses
                }).ToList()
            };

            return ApiResponse<TeacherWithScheduleOutput>.SuccessResponse(HttpStatusCode.OK,
                    _localizer["Gnrl.Successful"], teacherWithScheduleOutput);
        }
    }
}
