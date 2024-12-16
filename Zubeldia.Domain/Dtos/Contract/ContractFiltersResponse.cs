namespace Zubeldia.Domain.Dtos.Contract
{
    using Grogu.Domain;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Commons;

    public class ContractFiltersResponse
    {
        public IEnumerable<KeyNameDto> Currencies { get; set; }
        public IEnumerable<KeyNameDto> Types
        {
            get
            {
                return EnumExtension.GetKeyNameFromEnum<ContractTypeEnum>();
            }
        }
    }
}
