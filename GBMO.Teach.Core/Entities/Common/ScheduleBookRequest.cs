using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Common
{
    public class ScheduleBookRequest : BaseEntity
    {
        public Guid StudenId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid ScheduleId { get; set; }
        public RequestStatusses Status { get; set; }
    }
}
