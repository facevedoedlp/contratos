namespace Zubeldia.Domain.Entities.Base
{
    public abstract class AuditableEntity : Entity<int>
    {
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; } = DateTime.Now;
        public virtual string? LastModificationBy { get; set; }
        public virtual DateTime? LastModificationDate { get; set; }
    }
}
