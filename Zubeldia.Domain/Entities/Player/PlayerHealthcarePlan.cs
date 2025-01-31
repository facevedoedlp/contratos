namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class PlayerHealthcarePlan : Entity<int>
    {
        public int PlayerId { get; set; }
        public int HealthcarePlanId { get; set; }
        public string AffiliateNumber { get; set; }
        public string? FrontPhoto { get; set; }
        public string? BackPhoto { get; set; }

        public virtual Player Player { get; set; }
        public virtual HealthcarePlan HealthcarePlan { get; set; }
    }
}