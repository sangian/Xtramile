using AutoMapper;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Cities
{
    public class MockCityService : CityService
    {
        private readonly CountryRepository countryRepository;
        private readonly CityRepository cityRepository;
        private readonly IMapper mapper;

        public MockCityService(CountryRepository countryRepository, CityRepository cityRepository, IMapper mapper)
        {
            this.countryRepository = countryRepository;
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public AppResultDto<IList<CityDto>> GetCitiesByCountry(GetCitiesByCountryRequest request)
        {
            var result = new AppResultDto<IList<CityDto>>();

            bool isValidCountry = countryRepository.IsValidCountryName(request.Country);

            if (!isValidCountry)
            {
                result.Succeeded = false;
                result.Status = 404;
                result.Errors = new string[] { "Invalid country name." };

                return result;
            }

            IList<City> cities = cityRepository.GetCitiesByCountry(request.Country);

            if (cities.Count > 0)
            {
                result.Succeeded = true;
                result.Status = 200;
                result.Data = mapper.Map<IList<CityDto>>(cities);
            }
            else
            {
                result.Succeeded = false;
                result.Status = 404;
                result.Errors = new string[] { "No cities found." };
            }

            return result;
        }
    }
}
