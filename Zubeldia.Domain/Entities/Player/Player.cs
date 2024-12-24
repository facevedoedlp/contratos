namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class Player : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public string Photo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DominanceEnum DominanceFoot { get; set; }
        public DominanceEnum DominanceEye { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public bool HasAgent { get; set; }
        public int PlayerBirthPlaceId { get; set; }
        public int? HealthcarePlanId { get; set; }
        public int DisciplineId { get; set; }
        public int CategoryId { get; set; }
        public int? CometId { get; set; }
        public int? FifaId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual HealthcarePlan HealthcarePlan { get; set; }
        public virtual PlayerBirthPlace BirthPlace { get; set; }
        public virtual ICollection<PlayerRelative> Relatives { get; set; } = new List<PlayerRelative>();
        public virtual ICollection<PlayerIdentification> Identifications { get; set; } = new List<PlayerIdentification>();
        public virtual ICollection<PlayerPosition> Positions { get; set; } = new List<PlayerPosition>();
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        public string GetFullName() => $"{LastName} {FirstName}";
        public string GetIdNumber() => Identifications?.First(e => e.Type == PlayerDoucmentTypeEnum.IdentityCard || e.Type == PlayerDoucmentTypeEnum.Passport).Number ?? string.Empty;
        public string GetCategory() => Category?.Name ?? string.Empty;
        public string GetNacionality() => BirthPlace?.Country?.Name ?? string.Empty;
        public string GetPositions() => string.Join(", ", Positions.Select(x => x.Position.Name));
    }
}