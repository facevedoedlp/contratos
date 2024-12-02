namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Domain.Dtos.Commons.Searches;

    public class GetContractsRequest : SearchCriteria
    {
        public ContractOrderPropertiesEnum? SortingProperty { get; set; }
    }
}
