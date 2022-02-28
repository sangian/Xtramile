using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.City
{
    public interface CityService
    {
        IEnumerable<AppResultDto<CityDto>> GetCitiesByCountry(GetCitiesByCountryRequest request);
    }
}
