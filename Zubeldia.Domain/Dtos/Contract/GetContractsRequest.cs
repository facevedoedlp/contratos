namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Dtos.Commons.Searches;

    public class GetContractsRequest : SearchCriteria
    {
        public string? Title { get; set; }
        public ContractOrderPropertiesEnum? SortingProperty { get; set; }
    }
}
