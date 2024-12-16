namespace Zubeldia.Domain.Entities
{
    using Zubeldia.Domain.Entities.Base;

    public class Player : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public string GetFullName() => $"{FirstName} {LastName}";
    }
}
