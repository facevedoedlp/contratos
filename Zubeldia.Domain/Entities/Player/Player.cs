namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class Player : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Photo { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public DominanceEnum DominanceFoot { get; set; }
        public DominanceEnum DominanceEye { get; set; }
        public GenderEnum Gender { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int DisciplineId { get; set; }
        public int CategoryId { get; set; }
        public int? AgentId { get; set; }
        public int? PlayerAddressId { get; set; }
        public int? PlayerHealthcarePlanId { get; set; }
        public int? CometId { get; set; }
        public int? FifaId { get; set; }
        public virtual Agent Agent { get; set; }
        public virtual Country Nacionality { get; set; }
        public virtual State State { get; set; }
        public virtual City City { get; set; }
        public virtual Category Category { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual PlayerHealthcarePlan HealthcarePlan { get; set; }
        public virtual PlayerAddress Address { get; set; }
        public virtual ICollection<PlayerRelative> Relatives { get; set; } = new List<PlayerRelative>();
        public virtual ICollection<PlayerIdentification> Identifications { get; set; } = new List<PlayerIdentification>();
        public virtual ICollection<PlayerPosition> Positions { get; set; } = new List<PlayerPosition>();
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
        public string GetFullName() => $"{LastName} {FirstName}";
        public string GetIdNumber() => Identifications?.FirstOrDefault(e => e.Type == PlayerDoucmentTypeEnum.IdentityCard || e.Type == PlayerDoucmentTypeEnum.Passport)?.Number ?? string.Empty;
        public string GetCategory() => Category?.Name ?? string.Empty;
        public string GetDiscipline() => Discipline?.Name ?? string.Empty;
        public string GetNacionality() => Nacionality?.Name ?? string.Empty;
        public string GetPositions() => Positions.Any() ? string.Join(", ", Positions.Select(x => x.Position.Name)) : string.Empty;

        public void Activate()
        {
            IsActive = true;
            DeactivationDate = null;
        }

        public void Deactivate()
        {
            IsActive = false;
            DeactivationDate = DateTime.Now;
        }
    }
}