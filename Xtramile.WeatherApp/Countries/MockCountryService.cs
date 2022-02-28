using AutoMapper;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Countries
{
    public class MockCountryService : CountryService
    {
        private readonly CountryRepository countryRepository;
        private readonly IMapper mapper;

        public MockCountryService(CountryRepository countryRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.mapper = mapper;
        }

        public AppResultDto<IList<CountryDto>> GetCountries()
        {
            var result = new AppResultDto<IList<CountryDto>>();

            IList<Country> countries = countryRepository.GetCountries();

            result.Succeeded = true;
            result.Status = 200;
            result.Data = mapper.Map<IList<CountryDto>>(countries);

            return result;
        }
    }
}
