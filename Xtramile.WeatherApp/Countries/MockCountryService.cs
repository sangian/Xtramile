using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Dtos;

namespace Xtramile.WeatherApp.Countries
{
    public class MockCountryService : CountryService
    {
        public IEnumerable<AppResultDto<CountryDto>> GetCountries()
        {
            throw new System.NotImplementedException();
        }
    }
}
