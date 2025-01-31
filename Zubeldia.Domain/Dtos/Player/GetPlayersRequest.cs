namespace Zubeldia.Domain.Dtos.Player
{
    using Zubeldia.Domain.Dtos.Commons.Searches;
    using Zubeldia.Domain.Dtos.Contract;

    public class GetPlayersRequest : SearchCriteria
    {
        public PlayerOrderPropertiesEnum? SortingProperty { get; set; }
    }
}
