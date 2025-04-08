﻿using GBMO.Teach.Core.Entities.Auth;

namespace GBMO.Teach.Core.Repositories.AuthRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetNotConnectedTeachersAsync(string studentId);
    }
}
