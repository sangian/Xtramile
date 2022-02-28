using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Country
{
    public interface CountryService
    {
        IEnumerable<AppResultDto<CountryDto>> GetCountries();
    }
}
