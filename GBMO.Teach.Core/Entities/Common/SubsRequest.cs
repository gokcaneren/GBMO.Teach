using GBMO.Teach.Core.Enums;

namespace GBMO.Teach.Core.Entities.Common
{
    public class SubsRequest : BaseEntity
    {
        public Guid StudenId { get; set; }
        public Guid TeacherId { get; set; }
        public SubRequestStatusses Status { get; set; }
    }
}
