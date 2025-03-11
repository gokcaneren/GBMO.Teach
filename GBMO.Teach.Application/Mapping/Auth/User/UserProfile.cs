using AutoMapper;
using GBMO.Teach.Application.Utilities;
using GBMO.Teach.Core.DTOs.Input.Auth.User;
using GBMO.Teach.Core.DTOs.Output.Auth.User;
using GBMO.Teach.Core.Entities.Auth;
using GBMO.Teach.Core.Repositories.AuthRepositories;

namespace GBMO.Teach.Application.Mapping.Auth.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegisterInput, Core.Entities.Auth.User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src=>PasswordManager.Hash(src.Password)))
                .AfterMap<UserRoleAction>();

            CreateMap<Core.Entities.Auth.User, UserLoginOutput>();
        }




        public class UserRoleAction : IMappingAction<UserRegisterInput, Core.Entities.Auth.User>
        {
            private readonly IRoleRepository _roleRepository;

            public UserRoleAction(IRoleRepository roleRepository)
            {
                _roleRepository = roleRepository;
            }

            public void Process(UserRegisterInput source, Core.Entities.Auth.User destination, ResolutionContext context)
            {
                var role = Task.Run(() => _roleRepository.GetBy(c => c.RoleTypeId == source.RoleTypeId))
                    .GetAwaiter().GetResult();

                destination.RoleId = role.Id;
            }
        }
    }
}
