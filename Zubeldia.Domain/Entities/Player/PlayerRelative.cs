namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Entities.Base;
    public class PlayerRelative : Entity<int>
    {
        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public FamilyRelationshipTypeEnum Relationship { get; set; }
        public Player Player { get; set; }
    }
}