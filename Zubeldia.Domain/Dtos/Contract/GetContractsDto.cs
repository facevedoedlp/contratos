namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Entities.Base;

    public class GetContractsDto : Entity<int>
    {
        public string Title { get; set; }
    }
}
