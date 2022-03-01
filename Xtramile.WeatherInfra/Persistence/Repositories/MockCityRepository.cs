using System.Collections.Generic;
using System.Linq;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;

namespace Xtramile.WeatherInfra.Persistence.Repositories
{
    public class MockCityRepository : CityRepository
    {
        public virtual List<City> Cities { get; private set; }

        public MockCityRepository()
        {
            Cities = new List<City>
            {
                new City { Id = 1, Name = "Sydney", Country = "Australia" },
                new City { Id = 2, Name = "Melbourne", Country = "Australia" },
                new City { Id = 3, Name = "Brisbane", Country = "Australia" },
                new City { Id = 4, Name = "Perth", Country = "Australia" },
                new City { Id = 5, Name = "Jakarta", Country = "Indonesia" },
                new City { Id = 6, Name = "Bandung", Country = "Indonesia" },
                new City { Id = 7, Name = "Surabaya", Country = "Indonesia" },
                new City { Id = 8, Name = "Yogyakarta", Country = "Indonesia" },
                new City { Id = 9, Name = "Kuala Lumpur", Country = "Malaysia" },
                new City { Id = 10, Name = "Malacca", Country = "Malaysia" },
                new City { Id = 11, Name = "Kuching", Country = "Malaysia" },
                new City { Id = 12, Name = "Johor Bahru", Country = "Malaysia" },
                new City { Id = 13, Name = "Punggol", Country = "Singapore" },
                new City { Id = 14, Name = "Bukit Panjang", Country = "Singapore" },
                new City { Id = 15, Name = "Serangoon", Country = "Singapore" },
                new City { Id = 16, Name = "Jurong East", Country = "Singapore" }
            };
        }

        public IList<City> GetCitiesByCountry(string country)
        {
            return Cities.Where(c => c.Country.ToLower().Equals(country.ToLower())).ToList();
        }
    }
}
