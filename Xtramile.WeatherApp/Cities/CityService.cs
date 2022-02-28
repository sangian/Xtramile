using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Cities
{
    public interface CityService
    {
        AppResultDto<IList<CityDto>> GetCitiesByCountry(GetCitiesByCountryRequest request);
    }
}
