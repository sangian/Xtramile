using Moq;
using System.Collections.Generic;
using Xtramile.WeatherApp.Common.Repositories;
using Xtramile.WeatherDomain.Entities;
using Xtramile.WeatherInfra.Persistence.Repositories;
using Xunit;

namespace Xtramile.WeatherAppTest
{
    public class CityRepositoryTest
    {
        public Mock<MockCityRepository> mock = new Mock<MockCityRepository>();

        [Theory]
        [InlineData("indonesia")]
        [InlineData("australia")]
        public void ShouldReturnCitiesOfCountry(string country)
        {
            mock.SetupGet(x => x.Cities).Returns(new List<City>
            {
                new City { Id = 1, Name = "Sydney", Country = "Australia" },
                new City { Id = 2, Name = "Melbourne", Country = "Australia" },
                new City { Id = 3, Name = "Brisbane", Country = "Australia" },
                new City { Id = 4, Name = "Perth", Country = "Australia" },
                new City { Id = 5, Name = "Jakarta", Country = "Indonesia" },
                new City { Id = 6, Name = "Bandung", Country = "Indonesia" },
                new City { Id = 7, Name = "Surabaya", Country = "Indonesia" },
                new City { Id = 8, Name = "Yogyakarta", Country = "Indonesia" }
            });

            CityRepository cityRepository = mock.Object;

            IList<City> cities = cityRepository.GetCitiesByCountry(country);

            Assert.Equal(4, cities.Count);
        }
    }
}
