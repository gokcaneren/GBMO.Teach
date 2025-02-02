namespace GBMO.Teach.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModifiedDate { get; set; } 
    public bool IsDeleted { get; set; }
    public DateTime? DeletedTime { get; set; }
}