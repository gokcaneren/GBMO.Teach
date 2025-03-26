﻿namespace GBMO.Teach.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
