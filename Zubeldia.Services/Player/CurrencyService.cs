namespace Zubeldia.Services.Player
{
    using AutoMapper;
    using Zubeldia.Domain.Dtos.Commons;
    using Zubeldia.Domain.Interfaces.Providers;
    using Zubeldia.Domain.Interfaces.Services;

    public class CurrencyService(ICurrencyDao currencyDao, IMapper mapper)
          : ICurrencyService
    {
        public async Task<IEnumerable<KeyNameDto>> GetDropdownAsync()
        {
            var currencies = await currencyDao.GetDropdownAsync();
            return mapper.Map<List<KeyNameDto>>(currencies);
        }
    }
}
