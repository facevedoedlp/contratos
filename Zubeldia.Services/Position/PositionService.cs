namespace Zubeldia.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class PositionService(IPositionDao positionDao, IMapper mapper)
          : IPositionService
    {
        public async Task<IEnumerable<KeyNameDto>> GetByDisciplineIdAsync(int disciplineId)
        {
            var disciplines = await positionDao.GetByDisciplineIdAsync(disciplineId);
            return mapper.Map<List<KeyNameDto>>(disciplines);
        }
    }
}
