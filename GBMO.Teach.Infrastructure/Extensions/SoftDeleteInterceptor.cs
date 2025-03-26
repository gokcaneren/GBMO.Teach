using GBMO.Teach.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GBMO.Teach.Infrastructure.Extensions
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
            int result, CancellationToken cancellationToken = default)
        {

            if(eventData.Context is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            var entries = eventData.Context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(c => c.State == EntityState.Deleted).ToList();

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedTime = DateTime.UtcNow;
            }


            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }


    }
}
