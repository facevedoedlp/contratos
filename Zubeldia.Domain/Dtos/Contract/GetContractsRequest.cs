namespace Zubeldia.Domain.Dtos.Contract
{
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Commons.Searches;

    public class GetContractsRequest : SearchCriteria
    {
        public int? CurrencyId { get; set; }
        public ContractTypeEnum? Type { get; set; }
        public ContractOrderPropertiesEnum? SortingProperty { get; set; }
    }
}
