using AutoMapper;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Cities
{
    public class MockCityService : CityService
    {
        private readonly CityRepository cityRepository;
        private readonly IMapper mapper;

        public MockCityService(CityRepository cityRepository, IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public AppResultDto<IList<CityDto>> GetCitiesByCountry(GetCitiesByCountryRequest request)
        {
            var result = new AppResultDto<IList<CityDto>>();

            IList<City> cities = cityRepository.GetCitiesByCountry(request.Country);

            result.Status = 200;
            result.Data = mapper.Map<IList<CityDto>>(cities);

            return result;
        }
    }
}
