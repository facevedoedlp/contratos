namespace Zubeldia.Services.State
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class StateService(IStateDao stateDao, IMapper mapper)
          : IStateService
    {
        public async Task<IEnumerable<KeyNameDto>> GetByCountryIdAsync(int countryId)
        {
            var states = await stateDao.GetByCountryId(countryId);
            return mapper.Map<List<KeyNameDto>>(states);
        }
    }
}
