using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Cities
{
    public class MockCityService : CityService
    {
        public IEnumerable<AppResultDto<CityDto>> GetCitiesByCountry(GetCitiesByCountryRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
