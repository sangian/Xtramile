using System.Collections.Generic;
using System.Linq;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherInfra.Persistence.Repositories
{
    public class MockCountryRepository : CountryRepository
    {
        private static List<Country> countries = new List<Country>
        {
            new Country { Id = 1, Name = "Australia" },
            new Country { Id = 2, Name = "Indonesia" },
            new Country { Id = 3, Name = "Malaysia" },
            new Country { Id = 4, Name = "Singapore" }
        };

        public IList<Country> GetCountries()
        {
            return countries;
        }

        public bool IsValidCountryName(string country)
        {
            return countries.Exists(c => c.Name.ToLower().Equals(country.ToLower()));
        }
    }
}
