using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Countries
{
    public interface CountryService
    {
        IEnumerable<AppResultDto<CountryDto>> GetCountries();
    }
}
