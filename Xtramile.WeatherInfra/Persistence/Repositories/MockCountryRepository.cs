using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherInfra.Persistence.Repositories
{
    public class MockCountryRepository : CountryRepository
    {
        public virtual List<Country> Countries { get; private set; }

        public MockCountryRepository()
        {
            Countries = new List<Country>
            {
                new Country { Id = 1, Name = "Australia" },
                new Country { Id = 2, Name = "Indonesia" },
                new Country { Id = 3, Name = "Malaysia" },
                new Country { Id = 4, Name = "Singapore" }
            };
        }

        public IList<Country> GetCountries()
        {
            return Countries;
        }

        public bool IsValidCountryName(string country)
        {
            return Countries.Exists(c => c.Name.ToLower().Equals(country.ToLower()));
        }
    }
}
