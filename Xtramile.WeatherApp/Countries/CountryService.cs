using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Countries
{
    public interface CountryService
    {
        AppResultDto<IList<CountryDto>> GetCountries();
    }
}
