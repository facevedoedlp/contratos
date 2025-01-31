namespace Zubeldia.Domain.Dtos.Player
{
    using Microsoft.AspNetCore.Http;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;

    public class CreatePlayerRequest : Entity<int?>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DominanceEnum DominanceFoot { get; set; }
        public DominanceEnum DominanceEye { get; set; }
        public GenderEnum Gender { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int? HealthcarePlanId { get; set; }
        public int DisciplineId { get; set; }
        public int CategoryId { get; set; }
        public int? AgentId { get; set; }
        public int? PlayerAddressId { get; set; }
        public int? PlayerHealthcarePlanId { get; set; }
        //public int? CometId { get; set; }
        //public int? FifaId { get; set; }
        public IEnumerable<int>? Positions { get; set; }
        public CreatePlayerHealthcarePlanRequest? HealthcarePlan { get; set; }
        public CreatePlayerAddressRequest? Address { get; set; }
        public IEnumerable<CreatePlayerRelativeRequest>? Relatives { get; set; }
        public IEnumerable<CreatePlayerIdentificationRequest>? Identifications { get; set; }
    }
    public class CreatePlayerIdentificationRequest : Entity<int?>
    {
        public string Number { get; set; }
        public PlayerDoucmentTypeEnum Type { get; set; }
        public DateTime ExpirationDate { get; set; }
        public IFormFile? FrontPhoto { get; set; }
        public IFormFile? BackPhoto { get; set; }
        public int CountryId { get; set; }
    }

    public class CreatePlayerRelativeRequest : Entity<int?>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public FamilyRelationshipTypeEnum Relationship { get; set; }
    }

    public class CreatePlayerAddressRequest : Entity<int?>
    {
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
    }

    public class CreatePlayerHealthcarePlanRequest : Entity<int?>
    {
        public int HealthcarePlanId { get; set; }
        public string AffiliateNumber { get; set; }
        public IFormFile? FrontPhoto { get; set; }
        public IFormFile? BackPhoto { get; set; }
    }
}
