namespace Zubeldia.Domain.Dtos.Player
{
    using System.Collections.Generic;
    using Grogu.Domain;
    using Zubeldia.Commons.Enums;
    using Zubeldia.Domain.Dtos.Commons;

    public class GetPlayerFormDropdownResponse
    {
        public IEnumerable<KeyNameDto> Countries { get; set; }
        public IEnumerable<KeyNameDto> Disciplines { get; set; }
        public IEnumerable<KeyNameDto> HealthcarePlans { get; set; }
        public IEnumerable<KeyNameDto> Agents { get; set; }
        public IEnumerable<KeyNameDto> Genders
        {
            get
            {
                return EnumExtension.GetKeyNameFromEnum<GenderEnum>();
            }
        }

        public IEnumerable<KeyNameDto> Dominances
        {
            get
            {
                return EnumExtension.GetKeyNameFromEnum<DominanceEnum>();
            }
        }

    }
}
