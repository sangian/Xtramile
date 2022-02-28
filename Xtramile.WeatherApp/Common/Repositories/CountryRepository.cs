using System.Collections.Generic;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherApp.Common.Repositories
{
    public interface CountryRepository
    {
        IList<Country> GetCountries();
    }
}
