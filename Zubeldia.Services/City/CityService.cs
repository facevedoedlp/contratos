namespace Zubeldia.Services.City
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class CityService(ICityDao cityDao, IMapper mapper)
          : ICityService
    {
        public async Task<IEnumerable<KeyNameDto>> GetByStateIdAsync(int stateId)
        {
            var cities = await cityDao.GetByStateIdAsync(stateId);
            return mapper.Map<List<KeyNameDto>>(cities);
        }
    }
}
