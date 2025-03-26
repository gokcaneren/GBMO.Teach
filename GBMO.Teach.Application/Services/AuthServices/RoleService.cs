using GBMO.Teach.Application.Services;
using GBMO.Teach.Core.DTOs.Output.Auth.Role;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Enums;
using GBMO.Teach.Core.Repositories;
using GBMO.Teach.Core.Utilities;
using GBMO.Teach.Localization.Resources;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace GBMO.Teach.Core.Services.AuthServices
{
    public class RoleService : Service<Role>, IRoleService
    {
        private readonly IStringLocalizer<SharedResources> _localizer;
        public RoleService(IGenericRepository<Role> repository, IStringLocalizer<SharedResources> localizer) : base(repository)
        {
            _localizer = localizer;
        }

        public async Task<ApiResponse<List<SimpleRoleOutput>>> SimpleRoleList()
        {
            var roleList = Enum.GetValues(typeof(RoleTypes))
                            .Cast<RoleTypes>()
                            .Where(role => role!=RoleTypes.Admin)
                            .Select(role => new SimpleRoleOutput { RoleTypeId = (int)role, Name = role.ToString() })
                            .ToList();

            return await Task.FromResult(ApiResponse<List<SimpleRoleOutput>>
                .SuccessResponse(System.Net.HttpStatusCode.OK, _localizer["Successful"], roleList));
        }
    }
}
